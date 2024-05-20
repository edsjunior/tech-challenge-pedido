using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Repositories
{
	public interface ICarrinhoPedidoRepository
	{
		Task<IEnumerable<Pedido>> GetAllAsync();
        Task<Pedido> GetByIdAsync(Guid id);
        Task<Pedido> AddAsync(Pedido pedido);
        Task<Pedido> UpdateAsync(Pedido pedido);
        Task<bool> DeleteAsync(Guid id);
	}
}
