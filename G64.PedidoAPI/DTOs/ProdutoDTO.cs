
namespace G64.PedidoAPI.DTOs
{
    public class ProdutoDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public TipoDTO Tipo { get; set; }

    }
    public enum TipoDTO
    {
        LANCHE,
        BEBIDA,
        ACOMPANHAMENTO
    }
}
