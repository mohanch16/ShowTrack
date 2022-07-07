
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShowTrack.Server.Models;

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

    public async Task<List<Show>> GetShows()
    {
        return await this.ShowsCollection.Find(_ => true).ToListAsync();
    }

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
    
    public async Task<List<Show>> SearchShows(string title)
    {
        if (String.IsNullOrWhiteSpace(title)) 
        {
            return new List<Show>();
        }
        
        var showTitleFilter = Builders<Show>.Filter.Eq("Title", title);
        return await this.ShowsCollection.Find(showTitleFilter).ToListAsync();
    }
}