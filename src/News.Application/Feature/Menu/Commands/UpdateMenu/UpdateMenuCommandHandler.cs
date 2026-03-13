using MediatR;
using News.Application.Exceptions;
using News.Application.Feature.Menus.Commands.UpdateMenu;

namespace News.Application.Feature.Menus.Commands.UpdateMenu;

public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand>
{
    private readonly IMenuRepository _repository;

    public UpdateMenuCommandHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await _repository.GetByIdAsync(request.Id);

        if (menu is null)
            throw new NotFoundException(nameof(menu), request.Id);

        menu.Name = request.Name;

        _repository.UpdateAsync(menu);
        await _repository.SaveChangesAsync();
    }
}
