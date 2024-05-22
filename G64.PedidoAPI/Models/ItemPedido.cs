namespace G64.PedidoAPI.Models
{
	public class ItemPedido
	{
		public Guid Id { get; set; }
		public string Descricao { get; set; }
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; }

        public List<Pedido> Pedidos { get; set; }
    }
}
