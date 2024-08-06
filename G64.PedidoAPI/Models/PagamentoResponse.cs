namespace G64.PedidoAPI.Models
{
    public class PagamentoResponse
    {
        public string Id { get; set; }
        public PedidoStatus Status { get; set; }
        public decimal Valor { get; set; }
        // Adicione outras propriedades conforme necessário
    }
}
