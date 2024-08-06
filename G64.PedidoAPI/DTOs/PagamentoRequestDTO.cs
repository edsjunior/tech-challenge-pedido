using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PagamentoRequestDTO
	{
		public string MetodoPagamento { get; set; }
		public decimal Valor { get; set; }
		public string NumeroPedido { get; set; }
		

	}
}

