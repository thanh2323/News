using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using News.Infrastructure.Data;
using News.Infrastructure.Repositories;
using System.Data;
using Microsoft.Data.SqlClient;

namespace News.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDbConnection>(sp => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();

        return services;
    }
}
