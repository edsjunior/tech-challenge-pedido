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
                    Status = table.Column<int>(type: "integer", nullable: false)
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
                    { new Guid("0d351c7a-be0b-400a-b454-4989c5e4ef38"), "Batata Frita", 7.99m, 1 },
                    { new Guid("3107f028-53d7-4f76-8d6d-a2445827aa4e"), "Coca-cola", 5.99m, 1 },
                    { new Guid("6fd9695c-a117-47bd-a557-806b79206808"), "Sorvete", 9.99m, 2 },
                    { new Guid("d35786a3-4c2a-45db-9edc-9d9df76736c4"), "Combo Whopper", 15.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "Status", "Total" },
                values: new object[,]
                {
                    { new Guid("4213ed74-149c-4927-9f26-f204a782ddbd"), new DateTime(2024, 8, 5, 0, 18, 21, 332, DateTimeKind.Utc).AddTicks(3078), 0, 29.97m },
                    { new Guid("7467bb40-e942-44d7-b32e-bb2972032f86"), new DateTime(2024, 8, 5, 0, 18, 21, 332, DateTimeKind.Utc).AddTicks(3090), 0, 19.98m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("0d351c7a-be0b-400a-b454-4989c5e4ef38"), new Guid("4213ed74-149c-4927-9f26-f204a782ddbd") },
                    { new Guid("3107f028-53d7-4f76-8d6d-a2445827aa4e"), new Guid("4213ed74-149c-4927-9f26-f204a782ddbd") },
                    { new Guid("d35786a3-4c2a-45db-9edc-9d9df76736c4"), new Guid("4213ed74-149c-4927-9f26-f204a782ddbd") },
                    { new Guid("6fd9695c-a117-47bd-a557-806b79206808"), new Guid("7467bb40-e942-44d7-b32e-bb2972032f86") }
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
