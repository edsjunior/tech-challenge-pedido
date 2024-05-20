using G64.ProdutoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace G64.ProdutoApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<Combo> Combos { get; set; }

        //Fluent Api

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Ingredientes
            modelBuilder.Entity<Ingrediente>().HasKey(c => c.Id);
            modelBuilder.Entity<Ingrediente>()
                        .Property(c => c.Descricao)
                        .HasMaxLength(255)
                        .IsRequired();

            var ingrediente1 = new Ingrediente { Id = Guid.NewGuid(), Descricao = "Tomate" };
            var ingrediente2 = new Ingrediente { Id = Guid.NewGuid(), Descricao = "Hamburguer" };
            modelBuilder.Entity<Ingrediente>().HasData(ingrediente1, ingrediente2);

            //Produtos
            modelBuilder.Entity<Produto>().HasKey(c => c.Id);

            modelBuilder.Entity<Produto>()
                    .Property(c => c.Nome)
                    .HasMaxLength(100)
                    .IsRequired();
            modelBuilder.Entity<Produto>()
                    .Property(c => c.Descricao)
                    .HasMaxLength(255)
                    .IsRequired();
            modelBuilder.Entity<Produto>()
                    .Property(c => c.Preco)
                    .HasPrecision(12, 2)
                    .IsRequired();

            var produto1 = new Produto { Id = Guid.NewGuid(), Nome = "Whopper", Preco = 15.99m, Descricao = "Carne bovina", Tipo = tipo.LANCHE };
            var produto2 = new Produto { Id = Guid.NewGuid(), Nome = "Coca-cola", Preco = 5.99m, Descricao = "Refrigerante", Tipo = tipo.BEBIDA };
            var produto3 = new Produto { Id = Guid.NewGuid(), Nome = "Batata Frita", Preco = 7.99m, Descricao = "Batata", Tipo = tipo.ACOMPANHAMENTO };
            modelBuilder.Entity<Produto>().HasData(produto1, produto2, produto3);

            //Combo
            var comboId = Guid.NewGuid();
            var comboId2 = Guid.NewGuid();
            modelBuilder.Entity<Combo>().HasData(new Combo { Id = comboId }, new Combo { Id = comboId2 });

            modelBuilder.Entity<Combo>().HasKey(c => c.Id);
            modelBuilder.Entity<Combo>()
                .HasMany(c => c.Produtos)
                .WithMany(p => p.Combos)
                .UsingEntity<Dictionary<string, object>>(
                "ComboProduto",
                j => j.HasOne<Produto>().WithMany().HasForeignKey("ProdutosId"),
                j => j.HasOne<Combo>().WithMany().HasForeignKey("CombosId"),
                j =>
                {
                    j.Property<Guid>("CombosId");
                    j.Property<Guid>("ProdutosId");
                    j.HasKey("CombosId", "ProdutosId");
                    j.HasData(
                        new { CombosId = comboId, ProdutosId = produto1.Id },
                        new { CombosId = comboId, ProdutosId = produto2.Id },
                        new { CombosId = comboId, ProdutosId = produto3.Id },
                        new { CombosId = comboId2, ProdutosId = produto1.Id },
                        new { CombosId = comboId2, ProdutosId = produto2.Id }
                    );
                });
        }
    }
}
