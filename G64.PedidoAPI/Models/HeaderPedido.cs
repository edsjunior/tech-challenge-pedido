namespace G64.PedidoAPI.Models
{
	public class HeaderPedido
	{
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public PedidoStatus Status { get; set; }
    }
    public enum PedidoStatus
    {
        PENDENTE,
        PROCESSANDO,
        CONCLUIDO,
        CANCELADO
    }

}
