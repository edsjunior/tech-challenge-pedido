namespace G64.PedidoAPI.DTOs
{
	public class HeaderPedidoDTO
	{

        public Guid Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public PedidoStatusDTO Status { get; set; }
    }
    public enum PedidoStatusDTO
    {
        PENDENTE,
        PROCESSANDO,
        CONCLUIDO,
        CANCELADO
    }

}
