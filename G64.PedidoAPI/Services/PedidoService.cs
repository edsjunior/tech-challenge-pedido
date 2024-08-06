using AutoMapper;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using Microsoft.EntityFrameworkCore;
namespace G64.PedidoAPI.Services
{
	public class PedidoService
	{
		private readonly IPedidoRepository _repository;
		private readonly IMapper _mapper;
		//private readonly PagamentoClient _pagamentoClient;

		public PedidoService(IPedidoRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
			//_pagamentoClient = pagamentoClient;
		}

		public async Task<IEnumerable<PedidoDTO>> GetAllPedidosAsync()
		{
			var pedidos = await _repository.GetAllAsync();
			return _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
		}

		public async Task<PedidoDTO> GetPedidoByIdAsync(Guid id)
		{
			var pedido = await _repository.GetByIdAsync(id);
			return _mapper.Map<PedidoDTO>(pedido);
		}

		public async Task<PedidoDTO> CreatePedidoAsync(PedidoDTO pedidoDTO)
		{
			var pedido = _mapper.Map<Pedido>(pedidoDTO);
			var createdPedido = await _repository.AddAsync(pedido);
			return _mapper.Map<PedidoDTO>(createdPedido);
		}

		public async Task<PedidoDTO> UpdatePedidoAsync(PedidoDTO pedidoDTO)
		{
			var pedido = _mapper.Map<Pedido>(pedidoDTO);
			var updatedPedido = await _repository.UpdateAsync(pedido);
			return _mapper.Map<PedidoDTO>(updatedPedido);
		}

		public async Task<bool> DeletePedidoAsync(Guid id)
		{
			var pedido = await _repository.GetByIdAsync(id);
			if (pedido == null)
			{
				return false;
			}
			return await _repository.DeleteAsync(id);
		}

		// public async Task<PedidoDTO> UpdatePedidoStatusAsync(Guid pedidoId, PedidoStatus initialStatus)
		// {
		// 	var pedido = await _repository.GetByIdAsync(pedidoId);
		// 	if (pedido == null)
		// 	{
		// 		return null;
		// 	}

		// 	var paymentRequest = new PagamentoRequestDTO { PedidoId = pedidoId, Status = initialStatus };
		// 	var response = await _pagamentoClient.ProcessPaymentAsync(paymentRequest);

		// 	if (response.IsSuccess)
		// 	{
		// 		pedido.Status = PedidoStatus.PREPARANDO;
		// 	}
		// 	else
		// 	{
		// 		pedido.Status = PedidoStatus.CANCELADO;
		// 	}

		// 	await _repository.UpdateAsync(pedido);
		// 	return _mapper.Map<PedidoDTO>(pedido);
		// }
	}

}
