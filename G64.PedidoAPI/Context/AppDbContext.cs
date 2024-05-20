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
			base.OnModelCreating(modelBuilder);
		}
	}
}
