using G64.PedidoAPI.DTOs;

namespace G64.PedidoAPI.Repositories
{
	public interface ICarrinhoPedidoRepository
	{
        Task<IEnumerable<CarrinhoPedidoDTO>> GetAll();
        Task<CarrinhoPedidoDTO> GetCarrinhoPedidoByUserIdAsync(string userId);
        Task<CarrinhoPedidoDTO> GetCarrinhoPedidoById(Guid Id);
        Task<CarrinhoPedidoDTO> UpdateCarrinhoPedidoAsync(CarrinhoPedidoDTO carrinhoPedido);
        Task<bool> CleanCarrinhoPedidoAsync(Guid id);
        Task<bool> DeleteItemCarrinhoAsync(Guid cartItemId);
    }
}
