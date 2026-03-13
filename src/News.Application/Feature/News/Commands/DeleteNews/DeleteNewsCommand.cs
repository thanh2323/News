using MediatR;

namespace News.Application.Feature.News.Commands.DeleteNews;

public record DeleteNewsCommand(int Id) : IRequest<Unit>;
