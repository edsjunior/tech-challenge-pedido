namespace G64.PedidoAPI.DTOs
{
	public class ItemPedidoDTO
	{
		public Guid uuid { get; set; }
		public string titulo { get; set; }
		public string categoria { get; set; }
		public string descricao { get; set; }
		public int quantidade { get; set; }
		public decimal valorPorUnidade { get; set; }
	}
}
