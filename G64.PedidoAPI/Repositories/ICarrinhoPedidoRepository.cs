using G64.PedidoAPI.DTOs;

namespace G64.PedidoAPI.Repositories
{
	public interface ICarrinhoPedidoRepository
	{
        Task<CarrinhoPedidoDTO> GetAll();
        Task<CarrinhoPedidoDTO> GetCarrinhoPedidoByUserIdAsync(string userId);
        Task<CarrinhoPedidoDTO> GetCartById(Guid Id);
        Task<CarrinhoPedidoDTO> UpdateCartAsync(CarrinhoPedidoDTO carrinhoPedido);
        Task<bool> CleanCarrinhoPedidoAsync(string userId);
        Task<bool> DeleteItemCarrinhoAsync(Guid cartItemId);
    }
}
