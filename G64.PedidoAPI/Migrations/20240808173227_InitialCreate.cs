using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G64.PedidoAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Endereco = table.Column<string>(type: "text", nullable: false),
                    Telefone = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.ClienteId);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    uuid = table.Column<Guid>(type: "uuid", nullable: false),
                    titulo = table.Column<string>(type: "text", nullable: false),
                    categoria = table.Column<string>(type: "text", nullable: false),
                    descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    valorPorUnidade = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    pedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valorTotal = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    statusPagamento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.pedidoId);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItemPedido",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemPedidosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoItemPedido", x => new { x.PedidosId, x.ItemPedidosId });
                    table.ForeignKey(
                        name: "FK_PedidoItemPedido_ItensPedidos_ItemPedidosId",
                        column: x => x.ItemPedidosId,
                        principalTable: "ItensPedidos",
                        principalColumn: "uuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItemPedido_Pedidos_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedidos",
                        principalColumn: "pedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "uuid", "categoria", "descricao", "quantidade", "titulo", "valorPorUnidade" },
                values: new object[,]
                {
                    { new Guid("68742585-ef87-445d-b2e1-1b249fecbccb"), "Lanche", "Combo Whopper", 1, "Whopper", 15.99m },
                    { new Guid("93e1bb7b-088b-4e99-bba3-d04e38ea904a"), "Bebida", "Coca-cola", 1, "Refrigerante", 5.99m },
                    { new Guid("a4678ce5-d08d-4501-88cb-e2572cfd10e1"), "Acompanhamento", "Batata Frita", 1, "Fritas", 7.99m },
                    { new Guid("f0251552-0692-4e58-925c-b0bdaa2418ad"), "Sobremesa", "Sorvete de flocos", 2, "Sorvete", 9.99m }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "pedidoId", "data", "status", "statusPagamento", "valorTotal" },
                values: new object[,]
                {
                    { new Guid("1ecf9905-59fe-4284-940a-720eceb220a6"), new DateTime(2024, 8, 8, 14, 32, 27, 169, DateTimeKind.Utc).AddTicks(8579), "PENDENTE", "PENDENTE", 29.97m },
                    { new Guid("254e95d0-a295-45fd-a515-61936c284fa4"), new DateTime(2024, 8, 8, 14, 32, 27, 169, DateTimeKind.Utc).AddTicks(8628), "PENDENTE", "PENDENTE", 19.98m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("68742585-ef87-445d-b2e1-1b249fecbccb"), new Guid("1ecf9905-59fe-4284-940a-720eceb220a6") },
                    { new Guid("93e1bb7b-088b-4e99-bba3-d04e38ea904a"), new Guid("1ecf9905-59fe-4284-940a-720eceb220a6") },
                    { new Guid("a4678ce5-d08d-4501-88cb-e2572cfd10e1"), new Guid("1ecf9905-59fe-4284-940a-720eceb220a6") },
                    { new Guid("f0251552-0692-4e58-925c-b0bdaa2418ad"), new Guid("254e95d0-a295-45fd-a515-61936c284fa4") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItemPedido_ItemPedidosId",
                table: "PedidoItemPedido",
                column: "ItemPedidosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PedidoItemPedido");

            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
