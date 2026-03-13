using MediatR;
using Dapper;
using System.Data;
using News.Application.DTOs.Menu;
using News.Application.Feature.Menus.Queries.GetMenuById;

namespace News.Application.Feature.Menus.Queries.GetMenuById;

public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto?>
{
    private readonly IDbConnection _connection;

    public GetMenuByIdQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<MenuDto?> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
    {
        var sql = "SELECT Id, Name FROM Menus WHERE Id = @Id";

        return await _connection.QueryFirstOrDefaultAsync<MenuDto>(sql, new { request.Id });
    }
}