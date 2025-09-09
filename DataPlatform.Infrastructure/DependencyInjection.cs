using DataPlatform.Infrastructure.Interfaces;
using DataPlatform.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataPlatform.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var provider = config["Database:Provider"];
            var useInternalDb = Convert.ToBoolean(config["Database:UseInternalDb"] ?? "false");
            
            // Select connection string based on configuration
            string postgresConn;
            if (useInternalDb)
            {
                postgresConn = config["ConnectionStrings:Postgres"];
            }
            else
            {
                postgresConn = config["ConnectionStrings:PostgresExternal"];
            }
            
            var sqlServerConn = config["ConnectionStrings:SqlServer"];

            if (provider?.ToLower() == "sqlserver")
            {
                // Register SQL Server concrete context
                services.AddDbContext<SqlServerDbContext>(options =>
                    options.UseSqlServer(sqlServerConn,
                        sqlOptions => sqlOptions.EnableRetryOnFailure()));

                // When someone asks for AppDbContext, give them SqlServerDbContext instance
                services.AddScoped<AppDbContext>(sp => sp.GetRequiredService<SqlServerDbContext>());
            }
            else if (provider?.ToLower() == "postgres")
            {
                // Register Postgres concrete context
                services.AddDbContext<PostgresDbContext>(options =>
                    options.UseNpgsql(postgresConn,
                        npgOptions => npgOptions.EnableRetryOnFailure()));

                // Map AppDbContext to PostgresDbContext
                services.AddScoped<AppDbContext>(sp => sp.GetRequiredService<PostgresDbContext>());
            }
            else
            {
                throw new Exception("Database Provider is not configured properly in appsettings.json file.");
            }

            // Register repositories (Infrastructure implementations)
            services.AddScoped<ITranslationRepository, TranslationRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();

            return services;
        }
    }
}