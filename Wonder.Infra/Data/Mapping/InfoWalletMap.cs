using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class InfoWalletMap: IEntityTypeConfiguration<InfoWallet>
    {
        public void Configure(EntityTypeBuilder<InfoWallet> builder)
        {
            builder.ToView("infowallet")
                .HasKey(i => i.IdInfo);
        }
    }
}