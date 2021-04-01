using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApplication.Domain.Entities;

namespace ProductApplication.Infra.Mappings
{
    internal class SupplierMapConfig : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.ToTable("SUPPLIERS");

            builder.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Trade)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.CNPJ)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(x => x.ContactEmail)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.Telephone)
                .IsRequired()
                .HasMaxLength(250);

            builder.OwnsOne(y => y.Address, address =>
            {
                address.Property(y => y.State).HasMaxLength(2)
                .IsRequired()
                .HasColumnName("AddressState");

                address.Property(y => y.ZipCode)
                .HasMaxLength(11)
                .IsRequired()
                .HasColumnName("AddressZipCode");

                address.Property(y => y.Number)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("AddressNumber");

                address.Property(y => y.Street)
                .HasMaxLength(250)
                .IsRequired()
                .HasColumnName("AddressStreet");

                address.Property(y => y.City)
                .HasMaxLength(250)
                .IsRequired()
                .HasColumnName("AddressCity");

                address.Property(y => y.Neighborhood)
                .HasMaxLength(250)
                .IsRequired()
                .HasColumnName("AddressNeighborhood");

                address.Property(y => y.Complement)
                .HasMaxLength(250)
                .HasColumnName("AddressComplement");

            });
        }
    }
}
