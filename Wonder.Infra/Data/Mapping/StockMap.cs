using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class StockMap: IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stock");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Code)
                .HasColumnName("Code")
                .HasColumnType("VARCHAR(10)")
                .IsRequired()
                .HasMaxLength(8);
            builder.Property(p => p.Active)
                .HasColumnName("Active")
                .HasColumnType("BOOLEAN")
                .HasDefaultValue(true)
                .IsRequired();
        }
    }
}