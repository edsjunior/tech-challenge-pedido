namespace G64.PedidoAPI.Models
{
	public class ItemPedido
	{
		public Guid uuid { get; set; }
		public string titulo { get; set; }
		public string categoria { get; set; }
		public string descricao { get; set; }
		public int quantidade { get; set; }
		public decimal valorPorUnidade { get; set; }

		public List<Pedido> Pedidos { get; set; }
	}
}
