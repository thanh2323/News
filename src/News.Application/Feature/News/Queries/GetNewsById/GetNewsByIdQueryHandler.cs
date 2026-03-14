using Dapper;
using MediatR;
using News.Application.DTOs.News;
using News.Application.Feature.News.Queries.GetNewsById;
using System.Data;

namespace News.Application.Feature.News.Queries.GetNewsById;

public class GetNewsByIdQueryHandler : IRequestHandler<GetNewsByIdQuery, NewsDto?>
{
    private readonly IDbConnection _connection;

    public GetNewsByIdQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<NewsDto?> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Title, Content, ImageUrl, CreatedDate, MenuId FROM News WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<NewsDto>(sql, new { Id = request.Id });
    }
}
