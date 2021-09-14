using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Wonder.Infra.Data.Context
{
    public class PostgreSqlContextFactory: IDesignTimeDbContextFactory<PostgreSqlContext>
    {
        PostgreSqlContext IDesignTimeDbContextFactory<PostgreSqlContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<PostgreSqlContext>();
            builder.UseNpgsql(configuration.GetConnectionString("WonderConnectionString"));

            return new PostgreSqlContext(builder.Options);
        }

    }
}