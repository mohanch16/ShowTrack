using ShowTrack.Server.Models;
using ShowTrack.Server.Services;
using ShowTrack.Shared.Models.AsyncFilters;
using Microsoft.AspNetCore.Mvc;
using ShowTrack.Shared.Models;

namespace ShowTrack.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShowsController : ControllerBase
{
    private readonly ShowsService showsService;
    public ShowsController(ShowsService showsService)
    {
        this.showsService = showsService;
    }

    [HttpGet]
    public async Task<List<Show>> GetShows() 
    {
        return await this.showsService.GetShows();                    
    }

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Show>> GetShow(string id)
    {
        var show = await this.showsService.GetShow(id);

        if (show is null)
        {
            return NotFound();
        }

        return show;
    }

    [HttpPost]
    public async Task<IActionResult> PostBook(Show newShow)
    {
        await this.showsService.CreateAsync(newShow);

        return CreatedAtAction(nameof(GetShow), new { id = newShow.Id }, newShow);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> UpdateBook(string id, Show updatedShow)
    {
        var show = await this.showsService.GetShow(id);

        if (show is null)
        {
            return NotFound();
        }

        updatedShow.Id = id;

        await this.showsService.UpdateAsync(id, updatedShow);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteShow(string id)
    {
        var show = await this.showsService.GetShow(id);

        if (show is null)
        {
            return NotFound();
        }

        await this.showsService.RemoveAsync(id);

        return NoContent();
    }

    [HttpPost("pagedresult")]
    public async Task<List<Show>> GetPagedFilteredShows([FromBody] PagedResult pagingOptions)
    {
        return await this.showsService.GetShows(pagingOptions.FilterOptionsDTO, pagingOptions.PageNumber, pagingOptions.PageSize);        
    }

    [HttpPost("pagedresultcount")]
    public async Task<long> GetPagedFilteredShowsCount([FromBody] FilterOptionsDTO filterOptions)
    {
        var recordsCount = await this.showsService.GetFilteredRecordsCount(filterOptions);
        return recordsCount;
    }
}