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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Total = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    MetodoPagamento = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoItemPedido_Pedidos_PedidosId",
                        column: x => x.PedidosId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[,]
                {
                    { new Guid("28147948-e6b8-4353-b918-30a99f4783a8"), "Coca-cola", 5.99m, 1 },
                    { new Guid("2d80516f-5b64-418f-9d31-b6bcd82915db"), "Combo Whopper", 15.99m, 1 },
                    { new Guid("42dcbb5f-7a47-46eb-9ee5-7e95ce74d580"), "Batata Frita", 7.99m, 1 },
                    { new Guid("7bf1630e-b05e-49a9-9a5d-781a9bb586b2"), "Sorvete", 9.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "MetodoPagamento", "Status", "Total" },
                values: new object[,]
                {
                    { new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d"), new DateTime(2024, 8, 6, 0, 59, 32, 930, DateTimeKind.Utc).AddTicks(2238), "PENDENTE", 0, 29.97m },
                    { new Guid("fa288cff-f2a6-4311-9025-3fe4404c02f9"), new DateTime(2024, 8, 6, 0, 59, 32, 930, DateTimeKind.Utc).AddTicks(2252), "PENDENTE", 0, 19.98m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("28147948-e6b8-4353-b918-30a99f4783a8"), new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d") },
                    { new Guid("2d80516f-5b64-418f-9d31-b6bcd82915db"), new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d") },
                    { new Guid("42dcbb5f-7a47-46eb-9ee5-7e95ce74d580"), new Guid("76b145ce-8d29-4c54-8bf3-10ca30684c2d") },
                    { new Guid("7bf1630e-b05e-49a9-9a5d-781a9bb586b2"), new Guid("fa288cff-f2a6-4311-9025-3fe4404c02f9") }
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
