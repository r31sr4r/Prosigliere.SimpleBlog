using Microsoft.EntityFrameworkCore;
using Prosigliere.SimpleBlog.Infra.Data.EF;

namespace Prosigliere.SimpleBlog.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("SimpleBlogDb");
        services.AddDbContext<ProsigliereSimpleBlogDbContext>(
            options =>  options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            )
        );

        return services;
    }

    public static WebApplication MigrateDatabase(
       this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<ProsigliereSimpleBlogDbContext>();
        dbContext.Database.Migrate();
        return app;
    }
}
