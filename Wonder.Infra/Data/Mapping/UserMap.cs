using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting.Internal;
using Wonder.Domain.Models;

namespace Wonder.Infra.Data.Mapping
{
    public class UserMap: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("WonderUsers")
                .HasKey(u => u.Id);
        }
    }
}