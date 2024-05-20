namespace G64.PedidoAPI.Models
{
    public class ItemPedido
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid HeaderPedidoId { get; set; }
        public Produto Produto { get; set; } = new Produto();
    }

    
}
