
namespace ShowTrack.Shared.Models.AsyncFilters;

public class PaginationOptions
{
    public int PageSize { get; set; } = 10;

    public int DefaultPageNumber { get; set; }

    public int CurrentPageNumber { get; set; } = 1;

    public long TotalRecordsCount { get; set; } = 0;

    public int TotalNumberOfPages 
    {
        get
        {            
            return this.TotalRecordsCount > 0 ? (int)Math.Ceiling((double)this.TotalRecordsCount / this.PageSize) : 0;
        }
    }

    public bool HasPreviousPage => this.CurrentPageNumber > 1;
    
    public bool HasNextPage => this.CurrentPageNumber < this.TotalNumberOfPages;

    public int PreviousRecordsCount => (this.CurrentPageNumber - 1) * this.PageSize;

    public int FirstRecordNumber 
    { 
        get
        {
            return this.PreviousRecordsCount + 1;   
        }
    }

    public int LastRecordNumber 
    { 
        get
        {
            return this.PreviousRecordsCount + this.PageSize;
        }
    }
}