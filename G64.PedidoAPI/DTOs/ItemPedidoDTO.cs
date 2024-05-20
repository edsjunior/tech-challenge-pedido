using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.DTOs
{
    public class ItemPedidoDTO
    {
        public Guid Id { get; set; }
        public ProdutoDTO Produto { get; set; } = new ProdutoDTO();
        public int Quantidade { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid HeaderPedidoId { get; set; }
    }

    
}
