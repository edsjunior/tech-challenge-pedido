namespace G64.PedidoAPI.DTOs
{
	public class CarrinhoPedidoDTO
	{
		public HeaderPedidoDTO HeaderPedido { get; set; } = new HeaderPedidoDTO();
        public IEnumerable<ItemPedidoDTO> ItemPedidos { get; set; } = Enumerable.Empty<ItemPedidoDTO>();
    }
}
