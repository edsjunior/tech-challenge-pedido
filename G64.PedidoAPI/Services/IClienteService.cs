using G64.PedidoAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G64.PedidoAPI.Services
{
	public interface IClienteService
	{
		Task<IEnumerable<ClienteDTO>> GetAllClientesAsync();
		Task<ClienteDTO> GetClienteByIdAsync(Guid id);
		Task<ClienteDTO> CreateClienteAsync(ClienteDTO clienteDTO);
		Task<ClienteDTO> UpdateClienteAsync(ClienteDTO clienteDTO);
		Task<bool> DeleteClienteAsync(Guid id);
	}
}
