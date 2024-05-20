
namespace G64.PedidoAPI.Models
{
	public class CarrinhoPedido
	{
		public HeaderPedido HeaderPedido { get; set; } = new HeaderPedido();
        public IEnumerable<ItemPedido> ItemPedidos { get; set; } = Enumerable.Empty<ItemPedido>();
    }
}
