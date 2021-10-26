using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class FavoritesMap: IEntityTypeConfiguration<StockFavorites>
    {
        public void Configure(EntityTypeBuilder<StockFavorites> builder)
        {
            builder.ToTable("StockFavorites")
                .HasKey(f => f.Id);
        }
    }
}