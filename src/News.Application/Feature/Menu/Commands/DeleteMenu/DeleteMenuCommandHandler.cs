using MediatR;
using News.Application.Exceptions;
using News.Application.Feature.Menus.Commands.DeleteMenu;

namespace News.Application.Feature.Menus.Commands.DeleteMenu;

public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand>
{
    private readonly IMenuRepository _repository;

    public DeleteMenuCommandHandler(IMenuRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
    {
        var menu = await _repository.GetByIdAsync(request.Id);

        if (menu is null)
            throw new NotFoundException(nameof(menu), request.Id);

        _repository.Delete(menu);
        await _repository.SaveChangesAsync();
    }
}
