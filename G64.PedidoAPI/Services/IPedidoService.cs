using G64.PedidoAPI.DTOs;

namespace G64.PedidoAPI.Services
{
	public interface IPedidoService
	{
		Task<IEnumerable<PedidoDTO>> GetAllPedidosAsync();
		Task<PedidoDTO> GetPedidoByIdAsync(Guid id);
		Task CreatePedidoAsync(PedidoDTO productDto);
		Task UpdatePedidoAsync(PedidoDTO productDto);
		Task DeletePedidoAsync(Guid id);
	}
}
