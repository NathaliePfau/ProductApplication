using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductApplication.Domain.Entities;

namespace ProductApplication.Infra.Mappings
{
    public class CategoryMapConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("CATEGORIES");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(250);

            builder.HasOne<Supplier>(x => x.SupplierCategory)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.IdSupplier);
        }
    }
}
