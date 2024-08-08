namespace G64.PedidoAPI.Models
{
	public class Pedido
	{
		public Guid pedidoId { get; set; }
		public DateTime data { get; set; }
		public decimal valorTotal { get; set; }
		public List<ItemPedido> items { get; set; } = new List<ItemPedido>();
		public String status { get; set; }

        public String statusPagamento { get; set; }

	}

	
	
}
