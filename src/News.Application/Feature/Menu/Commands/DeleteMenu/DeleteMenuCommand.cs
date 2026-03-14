using MediatR;

namespace News.Application.Feature.Menus.Commands.DeleteMenu;

public record DeleteMenuCommand(int Id) : IRequest;
