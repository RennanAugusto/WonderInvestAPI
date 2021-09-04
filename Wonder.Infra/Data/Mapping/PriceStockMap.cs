using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class PriceStockMap: IEntityTypeConfiguration<PriceStock>
    {
        public void Configure(EntityTypeBuilder<PriceStock> builder)
        {
            builder.ToTable("PriceStock");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Active)
                .HasColumnName("Active")
                .HasColumnType("BOOLEAN")
                .IsRequired();
            builder.Property(p => p.Price)
                .HasColumnType("DECIMAL(19,6)")
                .IsRequired();
            builder.Property(p => p.Date)
                .HasColumnType("TIMESTAMP")
                .IsRequired();
            builder.Property(p => p.IsCloseOpen)
                .HasColumnType("VARCHAR(25)")
                .IsRequired();
        }
    }
}