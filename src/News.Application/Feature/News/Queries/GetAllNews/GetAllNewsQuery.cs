using MediatR;
using News.Application.DTOs.News;

namespace News.Application.Feature.News.Queries.GetAllNews;

public record GetAllNewsQuery(int? MenuId = null) : IRequest<IEnumerable<NewsDto>>;
