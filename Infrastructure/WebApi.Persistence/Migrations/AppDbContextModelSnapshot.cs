﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.Persistence.Context;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApi.Domain.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 289, DateTimeKind.Local).AddTicks(5244),
                            IsDeleted = false,
                            Name = "Baby"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 289, DateTimeKind.Local).AddTicks(5256),
                            IsDeleted = false,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 289, DateTimeKind.Local).AddTicks(5265),
                            IsDeleted = true,
                            Name = "Shoes"
                        });
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 290, DateTimeKind.Local).AddTicks(716),
                            IsDeleted = false,
                            Name = "Elektronik",
                            ParentId = 0,
                            Priority = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 290, DateTimeKind.Local).AddTicks(719),
                            IsDeleted = false,
                            Name = "Moda",
                            ParentId = 0,
                            Priority = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 290, DateTimeKind.Local).AddTicks(722),
                            IsDeleted = false,
                            Name = "Bilgisayar",
                            ParentId = 1,
                            Priority = 1
                        },
                        new
                        {
                            Id = 4,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 290, DateTimeKind.Local).AddTicks(724),
                            IsDeleted = false,
                            Name = "Kadın",
                            ParentId = 2,
                            Priority = 1
                        });
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Detail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Details");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 292, DateTimeKind.Local).AddTicks(6382),
                            Description = "Et orta eos çarpan olduğu.",
                            IsDeleted = false,
                            Title = "Sevindi."
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 3,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 292, DateTimeKind.Local).AddTicks(6432),
                            Description = "Balıkhaneye dağılımı sıfat labore koyun.",
                            IsDeleted = true,
                            Title = "Oldular karşıdakine."
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 4,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 292, DateTimeKind.Local).AddTicks(6472),
                            Description = "Değerli koştum dergi ipsum aut.",
                            IsDeleted = false,
                            Title = "Sunt."
                        });
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BrandId = 1,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 296, DateTimeKind.Local).AddTicks(5585),
                            Description = "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J",
                            Discount = 8.128416445054770m,
                            IsDeleted = false,
                            Price = 484.29m,
                            Title = "Awesome Wooden Pants"
                        },
                        new
                        {
                            Id = 2,
                            BrandId = 3,
                            CreatedDate = new DateTime(2024, 3, 21, 21, 26, 56, 296, DateTimeKind.Local).AddTicks(5631),
                            Description = "The Football Is Good For Training And Recreational Purposes",
                            Discount = 2.137380266684570m,
                            IsDeleted = false,
                            Price = 866.34m,
                            Title = "Tasty Steel Chicken"
                        });
                });

            modelBuilder.Entity("WebApi.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Detail", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.Category", "Category")
                        .WithMany("Details")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Product", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.ProductCategory", b =>
                {
                    b.HasOne("WebApi.Domain.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Domain.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Category", b =>
                {
                    b.Navigation("Details");

                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("WebApi.Domain.Entities.Product", b =>
                {
                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
