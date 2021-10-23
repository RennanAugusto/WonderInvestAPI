using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class CompanyMap: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Active)
                .HasColumnName("Active")
                .HasColumnType("BOOLEAN")
                .HasDefaultValue(true)
                .IsRequired();
            builder.Property(p => p.Name)
                .HasColumnType("VARCHAR(100)")
                .IsRequired();
            builder.Property(p => p.Acting)
                .HasConversion<int>()
                .IsRequired();
            builder.Property(p => p.LogoBase64);
        }
    }
}