using Cf.Dotnet.Architecture.Domain.SeedWork;
using Cf.Dotnet.Architecture.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cf.Dotnet.Database;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, string sectionName = "SqliteConnection")
    {
       services
           .AddScoped<IDatabaseContext, DatabaseContext>()
            .AddDbContext<DatabaseContext>(config =>
            {
                config.UseSqlite(configuration.GetConnectionString(sectionName));
            });

        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
    
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, DatabaseContext>();
        return services;
    }
}