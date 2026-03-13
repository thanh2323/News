using MediatR;
using Dapper;
using System.Data;
using News.Application.DTOs.Menu;
using News.Application.Feature.Menus.Queries.GetMenus;

namespace News.Application.Feature.Menus.Queries.GetMenus;

public class GetMenusQueryHandler : IRequestHandler<GetMenusQuery, IEnumerable<MenuDto>>
{
    private readonly IDbConnection _connection;

    public GetMenusQueryHandler(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<MenuDto>> Handle(GetMenusQuery request, CancellationToken cancellationToken)
    {
        return await _connection.QueryAsync<MenuDto>("SELECT Id, Name FROM Menus");
    }
}