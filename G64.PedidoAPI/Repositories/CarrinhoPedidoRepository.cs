using G64.PedidoAPI.Models;
using G64.PedidoAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace G64.PedidoAPI.Repositories

{
	public class CarrinhoPedidoRepository : ICarrinhoPedidoRepository
	{
		private readonly AppDbContext _context;

		public CarrinhoPedidoRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Pedido>> GetAllAsync()
		{
			return await _context.Pedidos.Include(p => p.Itens).ToListAsync();
		}

		public async Task<Pedido> GetByIdAsync(Guid id)
		{
			return await _context.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<Pedido> AddAsync(Pedido pedido)
		{
			_context.Pedidos.Add(pedido);
			await _context.SaveChangesAsync();
			return pedido;
		}

		public async Task<Pedido> UpdateAsync(Pedido pedido)
		{
			_context.Pedidos.Update(pedido);
			await _context.SaveChangesAsync();
			return pedido;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var pedido = await _context.Pedidos.FindAsync(id);
			if (pedido != null)
			{
				_context.Pedidos.Remove(pedido);
				await _context.SaveChangesAsync();
				return true;
			}
			return false;
		}
	}
}
