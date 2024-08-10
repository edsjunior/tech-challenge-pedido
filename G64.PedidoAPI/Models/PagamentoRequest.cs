
namespace G64.PedidoAPI.Models
{
    public class PagamentoRequest
    {
		public string pedidoId { get; set; }
		public decimal valorTotal { get; set; }

		public List<ItemPedido> items { get; set; }
	}
}
