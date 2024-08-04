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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "TEXT", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Data = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", precision: 12, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PedidoItemPedido",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ItemPedidosId = table.Column<Guid>(type: "TEXT", nullable: false)
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
                values: new object[] { new Guid("86b22a19-dd75-4930-b0cd-98ae877ee7fc"), "Combo Whopper", 15.99m, 1 });

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[] { new Guid("901a933b-7b47-476b-a00b-061be2a5e505"), "Batata Frita", 7.99m, 1 });

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[] { new Guid("a8755a2a-aec2-4a13-8528-254a134e2ecd"), "Coca-cola", 5.99m, 1 });

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[] { new Guid("fc72b3b6-06f9-4770-9f0c-eb93bc2be940"), "Sorvete", 9.99m, 2 });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "Status", "Total" },
                values: new object[] { new Guid("792b2330-22d6-4001-a5fe-4c97c1d47a53"), new DateTime(2024, 8, 4, 2, 14, 28, 783, DateTimeKind.Local).AddTicks(7637), 0, 29.97m });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "Status", "Total" },
                values: new object[] { new Guid("99d692e8-3b89-48d2-8280-9d2ca5b35a60"), new DateTime(2024, 8, 4, 2, 14, 28, 783, DateTimeKind.Local).AddTicks(7649), 0, 19.98m });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[] { new Guid("86b22a19-dd75-4930-b0cd-98ae877ee7fc"), new Guid("792b2330-22d6-4001-a5fe-4c97c1d47a53") });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[] { new Guid("901a933b-7b47-476b-a00b-061be2a5e505"), new Guid("792b2330-22d6-4001-a5fe-4c97c1d47a53") });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[] { new Guid("a8755a2a-aec2-4a13-8528-254a134e2ecd"), new Guid("792b2330-22d6-4001-a5fe-4c97c1d47a53") });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[] { new Guid("fc72b3b6-06f9-4770-9f0c-eb93bc2be940"), new Guid("99d692e8-3b89-48d2-8280-9d2ca5b35a60") });

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
