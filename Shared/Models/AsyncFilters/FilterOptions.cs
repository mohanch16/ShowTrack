namespace ShowTrack.Shared.Models.AsyncFilters;

public class FilterOptions
{
    public FilterOptions()
    {
        this.SelectedShowTypes = new HashSet<FilterItem>();
        this.SelectedSubscriptions = new HashSet<FilterItem>();
    }

    public IEnumerable<FilterItem> SelectedShowTypes { get; set; }
    public IEnumerable<FilterItem> SelectedSubscriptions { get; set; }

    public string SearchString { get; set; } = string.Empty;

    public void ResetFilters()
    {
        this.SelectedShowTypes = new HashSet<FilterItem>();
        this.SelectedSubscriptions = new HashSet<FilterItem>();
        this.SearchString = string.Empty;
    }

    public FilterOptionsDTO BuildDataTransferObject()
    {
        return new FilterOptionsDTO 
        {
            SelectedShowTypes = this.SelectedShowTypes.Select(fs => (ShowType)fs.Value),
            SelectedSubscriptions = this.SelectedSubscriptions.Select(fs => (SubscriptionType)fs.Value),
            SearchString = this.SearchString
        };
    }
}

public class FilterOptionsDTO
{
    public FilterOptionsDTO()
    {
        this.SelectedShowTypes = new List<ShowType>();
        this.SelectedSubscriptions = new List<SubscriptionType>();
    }

    public IEnumerable<ShowType> SelectedShowTypes { get; set; }
    public IEnumerable<SubscriptionType> SelectedSubscriptions { get; set; }

    public string SearchString { get; set; } = string.Empty;
}