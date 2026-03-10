using MediatR;
using Microsoft.AspNetCore.Mvc;
using News.Application.Feature.Menus.Commands.CreateMenu;
using News.Application.Feature.Menus.Commands.DeleteMenu;
using News.Application.Feature.Menus.Commands.UpdateMenu;
using News.Application.Feature.Menus.Queries.GetMenuById;
using News.Application.Feature.Menus.Queries.GetMenus;

namespace News.API.Controller;

[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly ISender _sender;

    public MenuController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenus()
    {
        var result = await _sender.Send(new GetMenusQuery());

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMenu(int id)
    {
        var result = await _sender.Send(new GetMenuByIdQuery(id));

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
    {
        var id = await _sender.Send(command);

        return CreatedAtAction(nameof(GetMenu), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMenuRequest request)
    {
        await _sender.Send(new UpdateMenuCommand(id, request.Name));

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sender.Send(new DeleteMenuCommand(id));

        return NoContent();
    }
}