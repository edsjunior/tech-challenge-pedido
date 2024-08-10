using G64.PedidoAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G64.PedidoAPI.Repositories
{
	public interface IClienteRepository
	{
		Task<IEnumerable<Cliente>> GetAllAsync();
		Task<Cliente> GetByIdAsync(Guid id);
		Task AddAsync(Cliente cliente);
		Task UpdateAsync(Cliente cliente);
		Task DeleteAsync(Guid id);
	}
}
