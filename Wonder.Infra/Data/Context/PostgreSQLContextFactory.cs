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
            var connectionString =
                "User ID=postgres; Password=root;Server=localhost;Port=5432;Database=WonderInvest;Integrated Security=true; Pooling=true;";

            builder.UseNpgsql(connectionString);

            return new PostgreSqlContext(builder.Options);
        }
        
    }
}