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
                            Id = new Guid("2d80516f-5b64-418f-9d31-b6bcd82915db"),
                            Descricao = "Combo Whopper",
                            PrecoUnitario = 15.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("28147948-e6b8-4353-b918-30a99f4783a8"),
                            Descricao = "Coca-cola",
                            PrecoUnitario = 5.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("42dcbb5f-7a47-46eb-9ee5-7e95ce74d580"),
                            Descricao = "Batata Frita",
                            PrecoUnitario = 7.99m,
                            Quantidade = 1
                        },
                        new
                        {
                            Id = new Guid("7bf1630e-b05e-49a9-9a5d-781a9bb586b2"),
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

                    b.Property<string>("MetodoPagamento")
                        .IsRequired()
                        .HasColumnType("text");

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
                            Id = new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d"),
                            Data = new DateTime(2024, 8, 6, 0, 59, 32, 930, DateTimeKind.Utc).AddTicks(2238),
                            MetodoPagamento = "PENDENTE",
                            Status = 0,
                            Total = 29.97m
                        },
                        new
                        {
                            Id = new Guid("fa288cff-f2a6-4311-9025-3fe4404c02f9"),
                            Data = new DateTime(2024, 8, 6, 0, 59, 32, 930, DateTimeKind.Utc).AddTicks(2252),
                            MetodoPagamento = "PENDENTE",
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
                            PedidosId = new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d"),
                            ItemPedidosId = new Guid("2d80516f-5b64-418f-9d31-b6bcd82915db")
                        },
                        new
                        {
                            PedidosId = new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d"),
                            ItemPedidosId = new Guid("28147948-e6b8-4353-b918-30a99f4783a8")
                        },
                        new
                        {
                            PedidosId = new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d"),
                            ItemPedidosId = new Guid("42dcbb5f-7a47-46eb-9ee5-7e95ce74d580")
                        },
                        new
                        {
                            PedidosId = new Guid("fa288cff-f2a6-4311-9025-3fe4404c02f9"),
                            ItemPedidosId = new Guid("7bf1630e-b05e-49a9-9a5d-781a9bb586b2")
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
