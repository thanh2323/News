using Dapper;
using MediatR;
using News.Application.DTOs.News;
using News.Application.Feature.News.Queries.GetAllNews;
using System.Data;

namespace News.Application.Feature.News.Queries.GetAllNews;

public class GetAllNewsQueryHandler : IRequestHandler<GetAllNewsQuery, IEnumerable<NewsDto>>
{
    private readonly IDbConnection _connection;

    public GetAllNewsQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<NewsDto>> Handle(GetAllNewsQuery request, CancellationToken cancellationToken)
    {
        const string sql = "SELECT Id, Title, Content, ImageUrl, CreatedDate, MenuId FROM News";

        if (request.MenuId.HasValue)
        {
            return await _connection.QueryAsync<NewsDto>(sql + " WHERE MenuId = @MenuId", new { MenuId = request.MenuId });
        }

        return await _connection.QueryAsync<NewsDto>(sql);
    }
}
