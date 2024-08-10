using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PedidoDTO
	{
		public Guid pedidoId { get; set; }
		public DateTime data { get; set; }
		public decimal valorTotal { get; set; }
		public List<ItemPedidoDTO> items { get; set; }
		public String status { get; set; }

		public String statusPagamento { get; set; }

	}
}
