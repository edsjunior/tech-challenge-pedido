namespace G64.PedidoAPI.Models
{
	public class Pedido
	{
		public Guid Id { get; set; }
		public DateTime Data { get; set; }
		public decimal Total { get; set; }
		public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
		public PedidoStatus Status { get; set; }
	}

	public enum PedidoStatus
	{
		PENDENTE,
		PREPARANDO,
		CONCLUIDO,
		CANCELADO,
		ENTREGUE
	}
}
