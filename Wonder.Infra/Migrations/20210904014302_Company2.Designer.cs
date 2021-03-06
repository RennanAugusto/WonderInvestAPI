// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wonder.Infra.Data.Context;

namespace Wonder.Infra.Migrations
{
    [DbContext(typeof(PostgreSqlContext))]
    [Migration("20210904014302_Company2")]
    partial class Company2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Wonder.Domain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Acting")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOLEAN")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Wonder.Domain.Models.PriceStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("BOOLEAN")
                        .HasColumnName("Active");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("IsCloseOpen")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(19,6)");

                    b.Property<int>("StockId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.ToTable("PriceStock");
                });

            modelBuilder.Entity("Wonder.Domain.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BOOLEAN")
                        .HasDefaultValue(true)
                        .HasColumnName("Active");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("Code");

                    b.Property<int>("CompanyId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Wonder.Domain.Models.PriceStock", b =>
                {
                    b.HasOne("Wonder.Domain.Models.Stock", null)
                        .WithMany("PricesList")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Wonder.Domain.Models.Stock", b =>
                {
                    b.HasOne("Wonder.Domain.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Wonder.Domain.Models.Stock", b =>
                {
                    b.Navigation("PricesList");
                });
#pragma warning restore 612, 618
        }
    }
}
