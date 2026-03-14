using MediatR;
using News.Application.Exceptions;
using News.Application.Feature.News.Commands.UpdateNews;

namespace News.Application.Feature.News.Commands.UpdateNews;

public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, Unit>
{
    private readonly INewsRepository _repository;

    public UpdateNewsCommandHandler(INewsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await _repository.GetByIdAsync(request.Id);
        if (news is null)
            throw new NotFoundException(nameof(news), request.Id);

        news.Title = request.Title;
        news.Content = request.Content;
        news.ImageUrl = request.ImageUrl;

        _repository.Update(news);
        await _repository.SaveChangesAsync();

        return Unit.Value;
    }
}
