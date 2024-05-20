using G64.PedidoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace G64.PedidoAPI.Context
{
	public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<ItemPedido> ItemPedidos { get; set; }
        public DbSet<HeaderPedido> HeaderPedidos { get; set; }

		//Fluent API
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//Produto
			modelBuilder.Entity<Produto>()
				.HasKey(c => c.Id);

			modelBuilder.Entity<Produto>()
				.Property(c => c.Id)
				.ValueGeneratedNever();

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


        }

	}
}
