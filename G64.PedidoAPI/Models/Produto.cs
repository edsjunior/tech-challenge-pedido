namespace G64.PedidoAPI.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public Tipo Tipo { get; set; }

    }
    public enum Tipo
    {
        LANCHE,
        BEBIDA,
        ACOMPANHAMENTO
    }
}
