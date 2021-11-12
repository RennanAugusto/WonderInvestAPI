using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class RlcWalletTicketMap: IEntityTypeConfiguration<RlcWalletTicket>
    {
        public void Configure(EntityTypeBuilder<RlcWalletTicket> builder)
        {
            builder.ToTable("RlcWalletTicket")
                .HasKey(r => r.Id);
        }
    }
}