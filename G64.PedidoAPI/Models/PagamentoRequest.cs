namespace G64.PedidoAPI.Models
{
    public class PagamentoRequest
    {
        public string MetodoPagamento { get; set; }
        public decimal Valor { get; set; }
        public string NumeroPedido { get; set; }
        // Adicione outras propriedades conforme necessário
    }
}
