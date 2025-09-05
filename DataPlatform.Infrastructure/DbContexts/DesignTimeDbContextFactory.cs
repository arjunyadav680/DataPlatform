using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataPlatform.Infrastructure.DbContexts
{
    public class PostgresDbContextFactory : IDesignTimeDbContextFactory<PostgresDbContext>
    {
        public PostgresDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DataPlatform.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<PostgresDbContext>();
            var connectionString = configuration.GetConnectionString("Postgres");
            
            optionsBuilder.UseNpgsql(connectionString);

            return new PostgresDbContext(optionsBuilder.Options);
        }
    }

    public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../DataPlatform.API"))
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();
            var connectionString = configuration.GetConnectionString("SqlServer");
            
            optionsBuilder.UseSqlServer(connectionString);

            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}
