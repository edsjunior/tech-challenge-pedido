using G64.PedidoAPI.Context;
using G64.PedidoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G64.PedidoAPI.Repositories
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly AppDbContext _context;

		public ClienteRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Cliente>> GetAllAsync()
		{
			return await _context.Clientes.ToListAsync();
		}

		public async Task<Cliente> GetByIdAsync(Guid id)
		{
			return await _context.Clientes.FindAsync(id);
		}

		public async Task AddAsync(Cliente cliente)
		{
			_context.Clientes.Add(cliente);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Cliente cliente)
		{
			_context.Clientes.Update(cliente);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var cliente = await _context.Clientes.FindAsync(id);
			if (cliente != null)
			{
				_context.Clientes.Remove(cliente);
				await _context.SaveChangesAsync();
			}
		}
	}
}
