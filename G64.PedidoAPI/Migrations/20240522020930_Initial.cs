using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G64.PedidoAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItensPedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedidos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Data = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PedidoItemPedido",
                columns: table => new
                {
                    PedidosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ItemPedidosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
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
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[,]
                {
                    { new Guid("1ba15591-fc3b-4613-8aa3-051e6e60e17d"), "Coca-cola", 5.99m, 1 },
                    { new Guid("272e6795-a488-4d52-a28c-363044567ef9"), "Batata Frita", 7.99m, 1 },
                    { new Guid("4111eadd-a800-432b-a0c0-c310f8a39b7f"), "Combo Whopper", 15.99m, 1 },
                    { new Guid("900d293e-6190-4c76-b9d4-258f93877421"), "Sorvete", 9.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "Status", "Total" },
                values: new object[,]
                {
                    { new Guid("2676c7e1-cee2-4c19-828e-16c0f26f3b9c"), new DateTime(2024, 5, 21, 23, 9, 29, 826, DateTimeKind.Local).AddTicks(4943), 0, 19.98m },
                    { new Guid("b470c527-cea5-47e3-8e78-3808a618f815"), new DateTime(2024, 5, 21, 23, 9, 29, 826, DateTimeKind.Local).AddTicks(4924), 0, 29.97m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("900d293e-6190-4c76-b9d4-258f93877421"), new Guid("2676c7e1-cee2-4c19-828e-16c0f26f3b9c") },
                    { new Guid("1ba15591-fc3b-4613-8aa3-051e6e60e17d"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") },
                    { new Guid("272e6795-a488-4d52-a28c-363044567ef9"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") },
                    { new Guid("4111eadd-a800-432b-a0c0-c310f8a39b7f"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") }
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
