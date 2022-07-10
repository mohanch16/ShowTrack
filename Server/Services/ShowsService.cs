
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShowTrack.Server.Models;
using ShowTrack.Shared.Models;
using ShowTrack.Shared.Models.AsyncFilters;

namespace ShowTrack.Server.Services;

public class ShowsService
{
    public IMongoCollection<Show> ShowsCollection { get; }
    
    public ShowsService(IOptions<ShowsDatabaseSettings> databaseSettings)
    {
        var settings = databaseSettings.Value;
        var mongoClient = new MongoClient(settings.ConnectionString);
        var database = mongoClient.GetDatabase(settings.DatabaseName);  
        this.ShowsCollection = database.GetCollection<Show>(settings.CollectionName);       
    }
    
    #region Basic CRUD Operations
    public async Task<List<Show>> GetShows() => 
        await this.ShowsCollection.Find(_ => true).ToListAsync();    

    public async Task<Show> GetShow(string id)
    {
        return await this.ShowsCollection.Find(show => show.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Show newShow) =>
        await this.ShowsCollection.InsertOneAsync(newShow);

    public async Task UpdateAsync(string id, Show updatedShow) =>
        await this.ShowsCollection.ReplaceOneAsync(x => x.Id == id, updatedShow);

    public async Task RemoveAsync(string id) =>
        await this.ShowsCollection.DeleteOneAsync(x => x.Id == id);
    
    #endregion

    public async Task<List<Show>> GetShows(FilterOptionsDTO filterOptions, int currentPageNumber, int pageSize)
    {
        FilterDefinition<Show>? filterDef = this.BuildFilterDefinition(filterOptions);

        if (filterDef != null)
        {
            return await this.ShowsCollection.Find(filterDef)
                .Skip(pageSize * (currentPageNumber - 1))
                .Limit(pageSize).ToListAsync();
        }

        return await this.ShowsCollection.Find(_ => true)
                .Skip(pageSize * (currentPageNumber - 1))
                .Limit(pageSize).ToListAsync();
    }

    public async Task<long> GetFilteredRecordsCount(FilterOptionsDTO filterOptions)
    {
        FilterDefinition<Show>? filterDef = this.BuildFilterDefinition(filterOptions);

        if (filterDef != null)
        {
            return await this.ShowsCollection.CountDocumentsAsync(filterDef);
        }

        return await this.ShowsCollection.CountDocumentsAsync(_ => true);
    }
    
    private FilterDefinition<Show>? BuildFilterDefinition(FilterOptionsDTO filterOptions)
    {
        FilterDefinition<Show>? filterDef = null;
        if (!String.IsNullOrWhiteSpace(filterOptions.SearchString))
        {
            filterDef = Builders<Show>.Filter.Regex(widget => widget.Title, new BsonRegularExpression(filterOptions.SearchString, "i"));
        }

        if (filterOptions.SelectedShowTypes.Any())
        {
            if (filterDef != null)
            {
                filterDef &= Builders<Show>.Filter.In(widget => widget.Type, filterOptions.SelectedShowTypes);
            }
            else
            {
                filterDef = Builders<Show>.Filter.In(widget => widget.Type, filterOptions.SelectedShowTypes);
            }
        }

        if (filterOptions.SelectedSubscriptions.Any())
        {
            if (filterDef != null)
            {
                filterDef &= Builders<Show>.Filter.In(widget => widget.SubscriptionType, filterOptions.SelectedSubscriptions);
            }
            else
            {
                filterDef = Builders<Show>.Filter.In(widget => widget.SubscriptionType, filterOptions.SelectedSubscriptions);
            }
        }

        return filterDef;
    }

}