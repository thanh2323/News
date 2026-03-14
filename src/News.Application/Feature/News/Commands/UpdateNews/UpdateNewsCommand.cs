using MediatR;

namespace News.Application.Feature.News.Commands.UpdateNews;

public record UpdateNewsCommand(
    int Id,
    string Title,
    string Content,
    string? ImageUrl
) : IRequest<Unit>;
