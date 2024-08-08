using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
	public class PagamentoResponseDTO
	{
		public String uuid { get; set; }

        public String qrCode { get; set; }

        public String status { get; set; }

        public String pedidoId { get; set; }

        //public PedidoDTO Pedido { get; set; }
    }
}
