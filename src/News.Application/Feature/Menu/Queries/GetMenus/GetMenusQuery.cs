using MediatR;
using News.Application.DTOs.Menu;

namespace News.Application.Feature.Menus.Queries.GetMenus;

public record GetMenusQuery : IRequest<IEnumerable<MenuDto>>;