namespace G64.PedidoAPI.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Tipo Tipo { get; set; }

    }
    public enum Tipo
    {
        LANCHE,
        BEBIDA,
        ACOMPANHAMENTO
    }
}
