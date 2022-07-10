using ShowTrack.Shared.Models.AsyncFilters;

namespace ShowTrack.Shared.Models;

public class PagedResult
{
    public PagedResult()
    {
        this.FilterOptionsDTO = new FilterOptionsDTO();
    }

    public FilterOptionsDTO FilterOptionsDTO { get; set; }
    
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}