using System.Data.Entity;
using System.IO;
using Wonder.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Wonder.Infra.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Wonder.Infra.Data.Context
{
    public class PostgreSqlContext: IdentityDbContext<ApplicationUser>
    {
        private readonly IConfigurationRoot _configurationFile;
        private readonly string _connectionString;
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options)
        {
            _configurationFile = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            _connectionString = "User ID=postgres; Password=root;Server=localhost;Port=5432;Database=WonderInvest;Integrated Security=true; Pooling=true;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        
        public Microsoft.EntityFrameworkCore.DbSet<Company> Companies { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PriceStock> PriceStocks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Stock> Stocks { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ApplicationUser> Users { get; set; }
        
        public Microsoft.EntityFrameworkCore.DbSet<StockFavorites> StockFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Stock>(new StockMap().Configure);
            builder.Entity<PriceStock>(new PriceStockMap().Configure);
            builder.Entity<Company>(new CompanyMap().Configure);
            builder.Entity<ApplicationUser>(new UserMap().Configure);
            builder.Entity<Wallet>(new WalletMap().Configure);
            builder.Entity<RlcWalletTicket>(new RlcWalletTicketMap().Configure);
            builder.Entity<StockFavorites>(new FavoritesMap().Configure);
        }
    }
}