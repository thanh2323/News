using MediatR;
using News.Application.DTOs.Menu;

namespace News.Application.Feature.Menus.Queries.GetMenuById;

public record GetMenuByIdQuery(int Id) : IRequest<MenuDto?>;