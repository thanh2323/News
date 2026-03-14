using MediatR;
using News.Application.Exceptions;


namespace News.Application.Feature.News.Commands.DeleteNews;

public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, Unit>
{
    private readonly INewsRepository _newsRepository;

    public DeleteNewsCommandHandler(INewsRepository repository)
    {
        _newsRepository = repository;
    }

    public async Task<Unit> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await _newsRepository.GetByIdAsync(request.Id);
        if (news is null)
            throw new NotFoundException(nameof(news), request.Id);

        _newsRepository.Delete(news);
        await _newsRepository.SaveChangesAsync();

        return Unit.Value;
    }
}
