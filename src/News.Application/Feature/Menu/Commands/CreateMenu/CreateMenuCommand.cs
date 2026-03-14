using MediatR;

namespace News.Application.Feature.Menus.Commands.CreateMenu;

public record CreateMenuCommand(string Name) : IRequest<int>;