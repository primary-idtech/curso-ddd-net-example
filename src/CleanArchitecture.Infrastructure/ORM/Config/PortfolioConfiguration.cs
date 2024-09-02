using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.ORM.Config
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(e => e.Id)
                .UseIdentityColumn(1, 1)
                .ValueGeneratedOnAdd()
                .HasColumnOrder(1);

            builder.OwnsOne(e => e.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .IsRequired()
                    .HasColumnName("Name")
                    .HasColumnOrder(2);

                nameBuilder.HasIndex(n => n.Value).IsUnique();
            });

            builder.Property(e => e.Enabled).HasColumnOrder(3);
            builder.HasKey(e => e.Id).HasName("PK_Portfolio_Id");
        }
    }
}
