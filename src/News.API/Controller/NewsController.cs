using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.Application.Feature.News.Commands.CreateNews;
using News.Application.Feature.News.Commands.DeleteNews;
using News.Application.Feature.News.Commands.UpdateNews;
using News.Application.Feature.News.Queries.GetAllNews;
using News.Application.Feature.News.Queries.GetNewsById;

namespace News.API.Controller;

[ApiController]
[Route("api/news")]
public class NewsController : ControllerBase
{
    private readonly ISender _sender;

    public NewsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNews([FromQuery] int? menuId = null)
    {
        var result = await _sender.Send(new GetAllNewsQuery(menuId));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNews(int id)
    {
        var result = await _sender.Send(new GetNewsByIdQuery(id));
        if (result is null)
            return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNewsRequest request)
    {
        var command = new CreateNewsCommand(request.Title, request.Content, request.ImageUrl, request.MenuId);
        var id = await _sender.Send(command);
        return CreatedAtAction(nameof(GetNews), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateNewsRequest request)
    {
        var command = new UpdateNewsCommand(id, request.Title, request.Content, request.ImageUrl);
        await _sender.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sender.Send(new DeleteNewsCommand(id));
        return NoContent();
    }
}
