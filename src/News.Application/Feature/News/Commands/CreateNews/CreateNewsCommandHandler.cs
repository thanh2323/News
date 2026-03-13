using MediatR;
using News.Application.Exceptions;
using News.Application.Feature.News.Commands.CreateNews;
using News.Domain.Entities;

namespace News.Application.Feature.News.Commands.CreateNews;

public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, int>
{
    private readonly INewsRepository _newsRepository;
    private readonly IMenuRepository _menuRepository;

    public CreateNewsCommandHandler(INewsRepository newsRepository, IMenuRepository menuRepository)
    {
        _newsRepository = newsRepository;
        _menuRepository = menuRepository;
    }

    public async Task<int> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var menu = await _menuRepository.GetByIdAsync(request.MenuId);
        if (menu is null)
            throw new NotFoundException(nameof(menu), request.MenuId);

        var news = new NewsList
        {
            Title = request.Title,
            Content = request.Content,
            ImageUrl = request.ImageUrl,
            MenuId = request.MenuId,
            CreatedDate = DateTime.Now
        };

        await _newsRepository.AddAsync(news);
        await _newsRepository.SaveChangesAsync();

        return news.Id;
    }
}
