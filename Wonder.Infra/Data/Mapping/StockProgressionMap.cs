using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class StockProgressionMap: IEntityTypeConfiguration<StockProgression>
    {
        public void Configure(EntityTypeBuilder<StockProgression> builder)
        {
            builder.ToView("stockprogression")
                .HasKey(s => s.Id);
        }
    }
}