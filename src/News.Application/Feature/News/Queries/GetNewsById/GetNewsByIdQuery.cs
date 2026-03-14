using MediatR;
using News.Application.DTOs.News;

namespace News.Application.Feature.News.Queries.GetNewsById;

public record GetNewsByIdQuery(int Id) : IRequest<NewsDto?>;
