using G64.PedidoAPI.Models;
using G64.PedidoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace G64.PedidoAPI.Repositories

{
	public class PedidoRepository : IPedidoRepository
	{
		private readonly AppDbContext _context;

		public PedidoRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Pedido>> GetAllAsync()
		{
			return await _context.Pedidos.Include(p => p.items).ToListAsync();
		}

		public async Task<Pedido> GetByIdAsync(Guid id)
		{
			return await _context.Pedidos.Include(p => p.items).AsNoTracking().FirstOrDefaultAsync(p => p.pedidoId == id);
		}

		public async Task<Pedido> AddAsync(Pedido pedido)
		{
			_context.Pedidos.Add(pedido);
			await _context.SaveChangesAsync();
			//DetachEntity(pedido); // Desanexar o pedido após a criação
			DetachAllEntities();
			return pedido;
		}

		public async Task<Pedido> UpdateAsync(Pedido pedido)
		{
			var pedidoNoBanco = await GetByIdAsync(pedido.pedidoId);

			if (pedidoNoBanco != null)
			{
				//DetachEntity(pedidoNoBanco);
				DetachAllEntities();
			}

			_context.Attach(pedido);
			_context.Entry(pedido).State = EntityState.Modified;
			_context.Pedidos.Update(pedido);
			await _context.SaveChangesAsync();
			return pedido;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var pedido = await GetByIdAsync(id);
			if (pedido != null)
			{
				_context.Entry(pedido).State = EntityState.Modified;
				_context.Pedidos.Remove(pedido);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}

		private void DetachEntity(object entity)
		{
			var entry = _context.Entry(entity);
			if (entry != null)
			{
				entry.State = EntityState.Detached;
			}
		}

		private void DetachAllEntities()
		{
			var entries = _context.ChangeTracker.Entries().ToList();
			foreach (var entry in entries)
			{
				entry.State = EntityState.Detached;
			}
		}

	}
}
