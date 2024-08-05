﻿// <auto-generated />
using System;
using G64.PedidoAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace G64.PedidoAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("G64.PedidoAPI.Models.ItemPedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("PrecoUnitario")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ItensPedidos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d35786a3-4c2a-45db-9edc-9d9df76736c4"),
                            Descricao = "Combo Whopper",
                            PrecoUnitario = 15.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("3107f028-53d7-4f76-8d6d-a2445827aa4e"),
                            Descricao = "Coca-cola",
                            PrecoUnitario = 5.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("0d351c7a-be0b-400a-b454-4989c5e4ef38"),
                            Descricao = "Batata Frita",
                            PrecoUnitario = 7.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("6fd9695c-a117-47bd-a557-806b79206808"),
                            Descricao = "Sorvete",
                            PrecoUnitario = 9.99m,
                            Quantidade = 2
                        });
                });

            modelBuilder.Entity("G64.PedidoAPI.Models.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total")
                        .HasPrecision(12, 2)
                        .HasColumnType("numeric(12,2)");

                    b.HasKey("Id");

                    b.ToTable("Pedidos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("4213ed74-149c-4927-9f26-f204a782ddbd"),
                            Data = new DateTime(2024, 8, 5, 0, 18, 21, 332, DateTimeKind.Utc).AddTicks(3078),
                            Status = 0,
                            Total = 29.97m
                        },
                        new
                        {
                            Id = new Guid("7467bb40-e942-44d7-b32e-bb2972032f86"),
                            Data = new DateTime(2024, 8, 5, 0, 18, 21, 332, DateTimeKind.Utc).AddTicks(3090),
                            Status = 0,
                            Total = 19.98m
                        });
                });

            modelBuilder.Entity("PedidoItemPedido", b =>
                {
                    b.Property<Guid>("PedidosId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ItemPedidosId")
                        .HasColumnType("uuid");

                    b.HasKey("PedidosId", "ItemPedidosId");

                    b.HasIndex("ItemPedidosId");

                    b.ToTable("PedidoItemPedido");

                    b.HasData(
                        new
                        {
                            PedidosId = new Guid("4213ed74-149c-4927-9f26-f204a782ddbd"),
                            ItemPedidosId = new Guid("d35786a3-4c2a-45db-9edc-9d9df76736c4")
                        },
                        new
                        {
                            PedidosId = new Guid("4213ed74-149c-4927-9f26-f204a782ddbd"),
                            ItemPedidosId = new Guid("3107f028-53d7-4f76-8d6d-a2445827aa4e")
                        },
                        new
                        {
                            PedidosId = new Guid("4213ed74-149c-4927-9f26-f204a782ddbd"),
                            ItemPedidosId = new Guid("0d351c7a-be0b-400a-b454-4989c5e4ef38")
                        },
                        new
                        {
                            PedidosId = new Guid("7467bb40-e942-44d7-b32e-bb2972032f86"),
                            ItemPedidosId = new Guid("6fd9695c-a117-47bd-a557-806b79206808")
                        });
                });

            modelBuilder.Entity("PedidoItemPedido", b =>
                {
                    b.HasOne("G64.PedidoAPI.Models.ItemPedido", null)
                        .WithMany()
                        .HasForeignKey("ItemPedidosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G64.PedidoAPI.Models.Pedido", null)
                        .WithMany()
                        .HasForeignKey("PedidosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
