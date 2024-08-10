using Microsoft.EntityFrameworkCore;
using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Context
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Pedido> Pedidos { get; set; }
		public DbSet<ItemPedido> ItensPedidos { get; set; }
		public DbSet<Cliente> Clientes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);

			//Produtos
			modelBuilder.Entity<ItemPedido>().HasKey(c => c.uuid);

			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.descricao)
					.HasMaxLength(100)
					.IsRequired();
			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.quantidade)
					.IsRequired();
			modelBuilder.Entity<ItemPedido>()
					.Property(c => c.valorPorUnidade)
					.HasPrecision(12, 2)
					.IsRequired();

			modelBuilder.Entity<Pedido>()
				.Property(c => c.valorTotal)
				.HasPrecision(12, 2)
				.IsRequired();

			modelBuilder.Entity<Pedido>().Property(e => e.data)
					  .HasColumnType("timestamp with time zone")
					  .HasConversion(
						  v => v,
						  v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
					  );

			//Seed data
			var produto1 = new ItemPedido { uuid = Guid.NewGuid(),titulo = "Whopper", categoria = "Lanche" , descricao = "Combo Whopper", quantidade = 1, valorPorUnidade = 15.99m };
			var produto2 = new ItemPedido { uuid = Guid.NewGuid(),titulo = "Refrigerante", categoria = "Bebida", descricao = "Coca-cola", quantidade = 1, valorPorUnidade = 5.99m };
			var produto3 = new ItemPedido { uuid = Guid.NewGuid(),titulo = "Fritas", categoria = "Acompanhamento", descricao = "Batata Frita", quantidade = 1, valorPorUnidade = 7.99m };

			var produto4 = new ItemPedido { uuid = Guid.NewGuid(),titulo = "Sorvete", categoria = "Sobremesa", descricao = "Sorvete de flocos", quantidade = 2, valorPorUnidade = 9.99m };
			modelBuilder.Entity<ItemPedido>().HasData(produto1, produto2, produto3, produto4);


			var pedidoId1 = Guid.NewGuid();
			var pedidoId2 = Guid.NewGuid();
			modelBuilder.Entity<Pedido>().HasData(
					new Pedido { pedidoId = pedidoId1, data = DateTime.Now, valorTotal = 29.97m, status = PedidoStatus.PENDENTE.ToString(), statusPagamento = PagamentoStatus.PENDENTE.ToString() },
					new Pedido { pedidoId = pedidoId2, data = DateTime.Now, valorTotal = 19.98m, status = PedidoStatus.PENDENTE.ToString(), statusPagamento = PagamentoStatus.PENDENTE.ToString() }
				);

			modelBuilder.Entity<Pedido>().HasKey(c => c.pedidoId);
			modelBuilder.Entity<Pedido>()
				.HasMany(c => c.items)
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
						new { PedidosId = pedidoId1, ItemPedidosId = produto1.uuid },
						new { PedidosId = pedidoId1, ItemPedidosId = produto2.uuid },
						new { PedidosId = pedidoId1, ItemPedidosId = produto3.uuid },
						new { PedidosId = pedidoId2, ItemPedidosId = produto4.uuid }
					);
				});




		}
	}
}
