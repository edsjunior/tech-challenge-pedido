using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PedidoDTO
	{
		public Guid Id { get; set; }
		public DateTime Data { get; set; }
		public decimal Total { get; set; }
		public List<ItemPedidoDTO> Itens { get; set; }
		public PedidoStatus Status { get; set; }

		public string MetodoPagamento { get; set; }
	}
}
