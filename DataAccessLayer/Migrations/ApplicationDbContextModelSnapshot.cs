﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Restoran.Entity.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Available")
                        .HasColumnType("bit");

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDishes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SubMenuId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SubMenuId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("Restoran.Entity.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Restoran.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DishJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Dish");

                    b.Property<Guid?>("ReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Restoran.Entity.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("Restoran.Entity.SubMenu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("SubMenus");
                });

            modelBuilder.Entity("Restoran.Entity.Dish", b =>
                {
                    b.HasOne("Restoran.Entity.SubMenu", "SybMenu")
                        .WithMany("Dishes")
                        .HasForeignKey("SubMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SybMenu");
                });

            modelBuilder.Entity("Restoran.Entity.Order", b =>
                {
                    b.HasOne("Restoran.Entity.Receipt", "Receipt")
                        .WithMany("Orders")
                        .HasForeignKey("ReceiptId");

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("Restoran.Entity.SubMenu", b =>
                {
                    b.HasOne("Restoran.Entity.Menu", "Menu")
                        .WithMany("SubMenus")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Restoran.Entity.Menu", b =>
                {
                    b.Navigation("SubMenus");
                });

            modelBuilder.Entity("Restoran.Entity.Receipt", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Restoran.Entity.SubMenu", b =>
                {
                    b.Navigation("Dishes");
                });
#pragma warning restore 612, 618
        }
    }
}
