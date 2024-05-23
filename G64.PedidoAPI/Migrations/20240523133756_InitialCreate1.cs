using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G64.PedidoAPI.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("900d293e-6190-4c76-b9d4-258f93877421"), new Guid("2676c7e1-cee2-4c19-828e-16c0f26f3b9c") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("1ba15591-fc3b-4613-8aa3-051e6e60e17d"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("272e6795-a488-4d52-a28c-363044567ef9"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("4111eadd-a800-432b-a0c0-c310f8a39b7f"), new Guid("b470c527-cea5-47e3-8e78-3808a618f815") });

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("1ba15591-fc3b-4613-8aa3-051e6e60e17d"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("272e6795-a488-4d52-a28c-363044567ef9"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("4111eadd-a800-432b-a0c0-c310f8a39b7f"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("900d293e-6190-4c76-b9d4-258f93877421"));

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: new Guid("2676c7e1-cee2-4c19-828e-16c0f26f3b9c"));

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: new Guid("b470c527-cea5-47e3-8e78-3808a618f815"));

            migrationBuilder.InsertData(
                table: "ItensPedidos",
                columns: new[] { "Id", "Descricao", "PrecoUnitario", "Quantidade" },
                values: new object[,]
                {
                    { new Guid("08c34cea-4933-4a5f-891f-b2269b048ea4"), "Batata Frita", 7.99m, 1 },
                    { new Guid("231811de-bdfe-474b-bc51-10e3ac3251e6"), "Combo Whopper", 15.99m, 1 },
                    { new Guid("62e9b1d1-f2fc-45b2-a398-08696e63d439"), "Sorvete", 9.99m, 2 },
                    { new Guid("b35e4a7a-5cfc-47f1-834a-d0e725903870"), "Coca-cola", 5.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Data", "Status", "Total" },
                values: new object[,]
                {
                    { new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720"), new DateTime(2024, 5, 23, 10, 37, 55, 865, DateTimeKind.Local).AddTicks(8375), 0, 29.97m },
                    { new Guid("4572888d-aebb-4405-a72f-3225953ab60c"), new DateTime(2024, 5, 23, 10, 37, 55, 865, DateTimeKind.Local).AddTicks(8388), 0, 19.98m }
                });

            migrationBuilder.InsertData(
                table: "PedidoItemPedido",
                columns: new[] { "ItemPedidosId", "PedidosId" },
                values: new object[,]
                {
                    { new Guid("08c34cea-4933-4a5f-891f-b2269b048ea4"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") },
                    { new Guid("231811de-bdfe-474b-bc51-10e3ac3251e6"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") },
                    { new Guid("b35e4a7a-5cfc-47f1-834a-d0e725903870"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") },
                    { new Guid("62e9b1d1-f2fc-45b2-a398-08696e63d439"), new Guid("4572888d-aebb-4405-a72f-3225953ab60c") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("08c34cea-4933-4a5f-891f-b2269b048ea4"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("231811de-bdfe-474b-bc51-10e3ac3251e6"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("b35e4a7a-5cfc-47f1-834a-d0e725903870"), new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720") });

            migrationBuilder.DeleteData(
                table: "PedidoItemPedido",
                keyColumns: new[] { "ItemPedidosId", "PedidosId" },
                keyValues: new object[] { new Guid("62e9b1d1-f2fc-45b2-a398-08696e63d439"), new Guid("4572888d-aebb-4405-a72f-3225953ab60c") });

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("08c34cea-4933-4a5f-891f-b2269b048ea4"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("231811de-bdfe-474b-bc51-10e3ac3251e6"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("62e9b1d1-f2fc-45b2-a398-08696e63d439"));

            migrationBuilder.DeleteData(
                table: "ItensPedidos",
                keyColumn: "Id",
                keyValue: new Guid("b35e4a7a-5cfc-47f1-834a-d0e725903870"));

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: new Guid("1eaf06ff-0d39-49c4-bb89-d4db583da720"));

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: new Guid("4572888d-aebb-4405-a72f-3225953ab60c"));

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
        }
    }
}
