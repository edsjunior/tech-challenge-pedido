using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace G64.ProdutoApi.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tipo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComboProduto",
                columns: table => new
                {
                    CombosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutosId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboProduto", x => new { x.CombosId, x.ProdutosId });
                    table.ForeignKey(
                        name: "FK_ComboProduto_Combos_CombosId",
                        column: x => x.CombosId,
                        principalTable: "Combos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboProduto_Produtos_ProdutosId",
                        column: x => x.ProdutosId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Descricao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProdutoId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredientes_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Combos",
                column: "Id",
                values: new object[]
                {
                    new Guid("0e43e883-5d79-4169-a7a9-353bba0247db"),
                    new Guid("19530dee-cb23-435b-806e-1894442cfa62")
                });

            migrationBuilder.InsertData(
                table: "Ingredientes",
                columns: new[] { "Id", "Descricao", "ProdutoId" },
                values: new object[,]
                {
                    { new Guid("472fa4a4-cc5e-4c29-9a6b-a05166e0e29f"), "Tomate", null },
                    { new Guid("dd20facb-f422-4927-82c3-8255d97984ec"), "Hamburguer", null }
                });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "Descricao", "Nome", "Preco", "Tipo" },
                values: new object[,]
                {
                    { new Guid("2a4b6d82-eed1-4893-900d-cbf6d4b609f9"), "Batata", "Batata Frita", 7.99m, 2 },
                    { new Guid("373e83fb-423c-47f4-b612-0932d8a2e248"), "Carne bovina", "Whopper", 15.99m, 0 },
                    { new Guid("da1eb2b2-e055-48c9-af2e-28ef31c4da33"), "Refrigerante", "Coca-cola", 5.99m, 1 }
                });

            migrationBuilder.InsertData(
                table: "ComboProduto",
                columns: new[] { "CombosId", "ProdutosId" },
                values: new object[,]
                {
                    { new Guid("0e43e883-5d79-4169-a7a9-353bba0247db"), new Guid("373e83fb-423c-47f4-b612-0932d8a2e248") },
                    { new Guid("0e43e883-5d79-4169-a7a9-353bba0247db"), new Guid("da1eb2b2-e055-48c9-af2e-28ef31c4da33") },
                    { new Guid("19530dee-cb23-435b-806e-1894442cfa62"), new Guid("2a4b6d82-eed1-4893-900d-cbf6d4b609f9") },
                    { new Guid("19530dee-cb23-435b-806e-1894442cfa62"), new Guid("373e83fb-423c-47f4-b612-0932d8a2e248") },
                    { new Guid("19530dee-cb23-435b-806e-1894442cfa62"), new Guid("da1eb2b2-e055-48c9-af2e-28ef31c4da33") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComboProduto_ProdutosId",
                table: "ComboProduto",
                column: "ProdutosId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredientes_ProdutoId",
                table: "Ingredientes",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComboProduto");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
