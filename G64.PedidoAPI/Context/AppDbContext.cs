using Microsoft.EntityFrameworkCore;
using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Pedido> Pedidos { get; set; }
		public DbSet<ItemPedido> ItensPedidos { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			//Produtos
			modelBuilder.Entity<ItemPedido>().HasKey(c => c.Id);

			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.Descricao)
					.HasMaxLength(100)
					.IsRequired();
			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.Quantidade)
					.IsRequired();
			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.PrecoUnitario)
					.HasPrecision(12, 2)
					.IsRequired();

			modelBuilder.Entity<Pedido>()
				.Property(c => c.Total)
				.HasPrecision(12, 2)
				.IsRequired();

			modelBuilder.Entity<Pedido>().Property(e => e.Data)
					  .HasColumnType("timestamp with time zone")
					  .HasConversion(
						  v => v,
						  v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
					  );

			//Seed data
			var produto1 = new ItemPedido { Id = Guid.NewGuid(), Descricao = "Combo Whopper", Quantidade = 1, PrecoUnitario = 15.99m };
			var produto2 = new ItemPedido { Id = Guid.NewGuid(), Descricao = "Coca-cola", Quantidade = 1, PrecoUnitario = 5.99m };
			var produto3 = new ItemPedido { Id = Guid.NewGuid(), Descricao = "Batata Frita", Quantidade = 1, PrecoUnitario = 7.99m };

			var produto4 = new ItemPedido { Id = Guid.NewGuid(), Descricao = "Sorvete", Quantidade = 2, PrecoUnitario = 9.99m };
			modelBuilder.Entity<ItemPedido>().HasData(produto1, produto2, produto3, produto4);


			var pedidoId1 = Guid.NewGuid();
			var pedidoId2 = Guid.NewGuid();
			modelBuilder.Entity<Pedido>().HasData(
					new Pedido { Id = pedidoId1, Data = DateTime.Now, Total = 29.97m, Status = PedidoStatus.PENDENTE, MetodoPagamento = "PENDENTE" },
					new Pedido { Id = pedidoId2, Data = DateTime.Now, Total = 19.98m, Status = PedidoStatus.PENDENTE, MetodoPagamento = "PENDENTE" }
				);

			modelBuilder.Entity<Pedido>().HasKey(c => c.Id);
			modelBuilder.Entity<Pedido>()
				.HasMany(c => c.Itens)
				.WithMany(p => p.Pedidos)
				  
				.UsingEntity<Dictionary<string, object>>(
				"PedidoItemPedido",
				j => j.HasOne<ItemPedido>().WithMany().HasForeignKey("ItemPedidosId"),
				j => j.HasOne<Pedido>().WithMany().HasForeignKey("PedidosId"),
				j =>
				{
					j.Property<Guid>("PedidosId");
					j.Property<Guid>("ItemPedidosId");
					j.HasKey("PedidosId", "ItemPedidosId");
					j.HasData(
						new { PedidosId = pedidoId1, ItemPedidosId = produto1.Id },
						new { PedidosId = pedidoId1, ItemPedidosId = produto2.Id },
						new { PedidosId = pedidoId1, ItemPedidosId = produto3.Id },
						new { PedidosId = pedidoId2, ItemPedidosId = produto4.Id }
					);
				});


			
			
		}
	}
}
