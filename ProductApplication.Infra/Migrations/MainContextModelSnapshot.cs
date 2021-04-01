﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductApplication.Infra.Context;

namespace ProductApplication.Infra.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductApplication.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("IdSupplier")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("IdSupplier");

                    b.ToTable("CATEGORIES");
                });

            modelBuilder.Entity("ProductApplication.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(18)
                        .IsUnicode(false)
                        .HasColumnType("varchar(18)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("Trade")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false)
                        .HasColumnType("varchar(250)");

                    b.HasKey("Id");

                    b.ToTable("SUPPLIERS");
                });

            modelBuilder.Entity("ProductApplication.Domain.Entities.Category", b =>
                {
                    b.HasOne("ProductApplication.Domain.Entities.Supplier", "SupplierCategory")
                        .WithMany("Categories")
                        .HasForeignKey("IdSupplier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SupplierCategory");
                });

            modelBuilder.Entity("ProductApplication.Domain.Entities.Supplier", b =>
                {
                    b.OwnsOne("ProductApplication.Domain.ComplexType.Address", "Address", b1 =>
                        {
                            b1.Property<int>("SupplierId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(250)
                                .IsUnicode(false)
                                .HasColumnType("varchar(250)")
                                .HasColumnName("AddressCity");

                            b1.Property<string>("Complement")
                                .HasMaxLength(250)
                                .IsUnicode(false)
                                .HasColumnType("varchar(250)")
                                .HasColumnName("AddressComplement");

                            b1.Property<string>("Neighborhood")
                                .IsRequired()
                                .HasMaxLength(250)
                                .IsUnicode(false)
                                .HasColumnType("varchar(250)")
                                .HasColumnName("AddressNeighborhood");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(50)
                                .IsUnicode(false)
                                .HasColumnType("varchar(50)")
                                .HasColumnName("AddressNumber");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(2)
                                .IsUnicode(false)
                                .HasColumnType("varchar(2)")
                                .HasColumnName("AddressState");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(250)
                                .IsUnicode(false)
                                .HasColumnType("varchar(250)")
                                .HasColumnName("AddressStreet");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(11)
                                .IsUnicode(false)
                                .HasColumnType("varchar(11)")
                                .HasColumnName("AddressZipCode");

                            b1.HasKey("SupplierId");

                            b1.ToTable("SUPPLIERS");

                            b1.WithOwner()
                                .HasForeignKey("SupplierId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ProductApplication.Domain.Entities.Supplier", b =>
                {
                    b.Navigation("Categories");
                });
#pragma warning restore 612, 618
        }
    }
}