﻿namespace G64.PedidoAPI.Models
{
	public class Cliente
	{
		public Guid ClienteId { get; set; }
		public string Nome { get; set; }
		public string Endereco { get; set; }
		public string Telefone { get; set; }
	}
}
