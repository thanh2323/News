using MediatR;
using News.Application.Exceptions;
using News.Application.Feature.Menus.Commands.CreateMenu;
using News.Domain.Entities;

namespace News.Application.Feature.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>
{
    private readonly IMenuRepository _repository;

    public CreateMenuCommandHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = new Menu
        {
            Name = request.Name
        };

        await _repository.AddAsync(menu);
        await _repository.SaveChangesAsync();

        return menu.Id;
    }
}