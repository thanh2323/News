using MediatR;

namespace News.Application.Feature.News.Commands.CreateNews;

public record CreateNewsCommand(
    string Title,
    string Content,
    string? ImageUrl,
    int MenuId
) : IRequest<int>;
