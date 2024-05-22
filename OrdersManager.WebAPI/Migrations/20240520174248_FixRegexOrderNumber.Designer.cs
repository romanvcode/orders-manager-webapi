﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagement.Entities;

#nullable disable

namespace OrdersManager.WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240520174248_FixRegexOrderNumber")]
    partial class FixRegexOrderNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderManagement.Entities.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            CustomerName = "John Doe",
                            OrderDate = new DateTime(2024, 5, 20, 20, 42, 48, 156, DateTimeKind.Local).AddTicks(6763),
                            OrderNumber = "ORD_2024_1",
                            TotalAmount = 66.5m
                        },
                        new
                        {
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            CustomerName = "Jane Smith",
                            OrderDate = new DateTime(2024, 5, 20, 20, 42, 48, 156, DateTimeKind.Local).AddTicks(6803),
                            OrderNumber = "ORD_2024_2",
                            TotalAmount = 225.8m
                        });
                });

            modelBuilder.Entity("OrderManagement.Entities.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal");

                    b.HasKey("OrderItemId");

                    b.ToTable("OrderItems", (string)null);

                    b.HasData(
                        new
                        {
                            OrderItemId = new Guid("d20882df-7fca-4ee8-88bb-37d2fc75e63f"),
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            ProductName = "Product A",
                            Quantity = 2,
                            TotalPrice = 20.00m,
                            UnitPrice = 10.00m
                        },
                        new
                        {
                            OrderItemId = new Guid("2e27b6a4-469d-4d7f-8b8b-54af129675fd"),
                            OrderId = new Guid("f4816224-70d6-4491-ac52-34f298ace16f"),
                            ProductName = "Product B",
                            Quantity = 3,
                            TotalPrice = 46.50m,
                            UnitPrice = 15.50m
                        },
                        new
                        {
                            OrderItemId = new Guid("24d71ac2-0a9c-4914-9fd3-13bc25d42694"),
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            ProductName = "Product C",
                            Quantity = 7,
                            TotalPrice = 25.00m,
                            UnitPrice = 25.40m
                        },
                        new
                        {
                            OrderItemId = new Guid("ac90b8bc-349d-43fd-87a6-6a7ed8057697"),
                            OrderId = new Guid("735886c0-faf3-49ca-9776-8a20b756f1cb"),
                            ProductName = "Product D",
                            Quantity = 4,
                            TotalPrice = 25.00m,
                            UnitPrice = 12.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
