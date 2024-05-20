namespace G64.PedidoAPI.DTOs
{
	public class ItemPedidoDTO
	{
		public Guid Id { get; set; }
		public string Descricao { get; set; }
		public int Quantidade { get; set; }
		public decimal PrecoUnitario { get; set; }
	}
}
