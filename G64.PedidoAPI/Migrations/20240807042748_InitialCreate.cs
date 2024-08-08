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
                    { new Guid("135d1439-b2b8-4010-926b-413cb8f638de"), "Acompanhamento", "Batata Frita", 1, "Fritas", 7.99m },
                    { new Guid("14823eaa-1181-4495-a061-cda21898d89d"), "Sobremesa", "Sorvete de flocos", 2, "Sorvete", 9.99m },
                    { new Guid("69d69f8b-d34e-4134-9169-1380a0bbd6a6"), "Lanche", "Combo Whopper", 1, "Whopper", 15.99m },
                    { new Guid("f8bf4fe4-1285-4a76-be05-6f3ce4e4f10d"), "Bebida", "Coca-cola", 1, "Refrigerante", 5.99m }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "pedidoId", "data", "status", "statusPagamento", "valorTotal" },
                values: new object[,]
                {
                    { new Guid("c85d425e-492a-4331-875f-fb31f067002f"), new DateTime(2024, 8, 7, 1, 27, 48, 242, DateTimeKind.Utc).AddTicks(493), "PENDENTE", "PENDENTE", 19.98m },
                    { new Guid("e69b4050-2153-423a-bdce-6afe5146af21"), new DateTime(2024, 8, 7, 1, 27, 48, 242, DateTimeKind.Utc).AddTicks(456), "PENDENTE", "PENDENTE", 29.97m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("14823eaa-1181-4495-a061-cda21898d89d"), new Guid("c85d425e-492a-4331-875f-fb31f067002f") },
                    { new Guid("135d1439-b2b8-4010-926b-413cb8f638de"), new Guid("e69b4050-2153-423a-bdce-6afe5146af21") },
                    { new Guid("69d69f8b-d34e-4134-9169-1380a0bbd6a6"), new Guid("e69b4050-2153-423a-bdce-6afe5146af21") },
                    { new Guid("f8bf4fe4-1285-4a76-be05-6f3ce4e4f10d"), new Guid("e69b4050-2153-423a-bdce-6afe5146af21") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoItemPedido_ItemPedidosId",
                table: "PedidoItemPedido",
                column: "ItemPedidosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoItemPedido");

            migrationBuilder.DropTable(
                name: "ItensPedidos");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
