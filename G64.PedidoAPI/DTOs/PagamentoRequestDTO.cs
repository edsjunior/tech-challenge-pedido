using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PagamentoRequestDTO
	{
		public Guid PedidoId { get; set; }
		public PedidoStatus Status { get; set; }
	}
}
