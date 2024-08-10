using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G64.PedidoAPI.Services
{
	public class ClienteService : IClienteService
	{
		private readonly IClienteRepository _repository;
		private readonly IMapper _mapper;

		public ClienteService(IClienteRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<ClienteDTO>> GetAllClientesAsync()
		{
			var clientes = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
		}

		public async Task<ClienteDTO> GetClienteByIdAsync(Guid id)
		{
			var cliente = await _repository.GetByIdAsync(id);
			return _mapper.Map<ClienteDTO>(cliente);
		}

		public async Task<ClienteDTO> CreateClienteAsync(ClienteDTO clienteDTO)
		{
			var cliente = _mapper.Map<Cliente>(clienteDTO);
			await _repository.AddAsync(cliente);
			return _mapper.Map<ClienteDTO>(cliente);
		}

		public async Task<ClienteDTO> UpdateClienteAsync(ClienteDTO clienteDTO)
		{
			var cliente = _mapper.Map<Cliente>(clienteDTO);
			await _repository.UpdateAsync(cliente);
			return _mapper.Map<ClienteDTO>(cliente);
		}

		public async Task<bool> DeleteClienteAsync(Guid id)
		{
			await _repository.DeleteAsync(id);
			return true;
		}
	}
}
