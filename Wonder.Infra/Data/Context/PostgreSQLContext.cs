using Wonder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Wonder.Infra.Data.Mapping;

namespace Wonder.Infra.Data.Context
{
    public class PostgreSqlContext: DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Stock>(new StockMap().Configure);
            builder.Entity<PriceStock>(new PriceStockMap().Configure);
            builder.Entity<Company>(new CompanyMap().Configure);
        }
    }
}