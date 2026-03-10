using MediatR;

namespace News.Application.Feature.Menus.Commands.UpdateMenu;

public record UpdateMenuCommand(int Id, string Name) : IRequest;
