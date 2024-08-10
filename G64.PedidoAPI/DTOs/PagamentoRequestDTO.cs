using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PagamentoRequestDTO
	{
		public string pedidoId { get; set; }
		public decimal valorTotal { get; set; }
		public List<ItemPedidoDTO> items { get; set; }

	}
}

