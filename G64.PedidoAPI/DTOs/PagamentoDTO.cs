namespace G64.PedidoAPI.DTOs
{
    public class PagamentoDTO
    {
        public Guid PedidoId { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }
        public DateTime DataPagamento { get; set; }
    }
}
