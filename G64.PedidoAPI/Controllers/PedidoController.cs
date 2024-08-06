using AutoMapper;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.PedidoAPI.Controllers
{
	[Route("api/pedidos")]
	[ApiController]
	public class PedidoController : ControllerBase
	{
		private readonly PedidoService _service;
		private readonly PagamentoService _pagamentoService;

		public PedidoController(PedidoService service)
		{
			_service = service;
		}

		// GET: api/pedidos
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
		{
			var pedidos = await _service.GetAllPedidosAsync();
			return Ok(pedidos);
		}

		// GET: api/pedidos/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<PedidoDTO>> GetById(Guid id)
		{
			var pedido = await _service.GetPedidoByIdAsync(id);
			if (pedido == null)
			{
				return NotFound();
			}
			return Ok(pedido);
		}

		// POST: api/pedidos
		// [HttpPost]
		// public async Task<ActionResult<PedidoDTO>> Create([FromBody] PedidoDTO pedidoDTO)
		// {
		// 	var createdPedido = await _service.CreatePedidoAsync(pedidoDTO);
		// 	return CreatedAtAction(nameof(GetById), new { id = createdPedido.Id }, createdPedido);
		// }


		// POST: api/pedidos
		[HttpPost]
		public async Task<ActionResult<PedidoDTO>> Create([FromBody] PedidoDTO pedidoDTO)
		{
			var createdPedido = await _service.CreatePedidoAsync(pedidoDTO);

			// Cria a solicitação de pagamento
			var pagamentoRequest = new PagamentoRequestDTO
			{
				MetodoPagamento = pedidoDTO.MetodoPagamento,
				Valor = createdPedido.Total,
				NumeroPedido = createdPedido.Id.ToString()
			};

			// Faz a chamada à API de pagamento
			var pagamentoResponse = await _pagamentoService.CriaPagamentoAsync(pagamentoRequest);

			// Atualiza o pedido com informações de pagamento
			createdPedido.Status = pagamentoResponse.Status;
			await _service.UpdatePedidoAsync(createdPedido);

			return CreatedAtAction(nameof(GetById), new { id = createdPedido.Id }, createdPedido);
		}



		// // PUT: api/pedidos/{id}
		// [HttpPut("{id}")]
		// public async Task<IActionResult> Update(Guid id, [FromBody] PedidoDTO pedidoDTO)
		// {
		// 	var existingPedido = await _service.GetPedidoByIdAsync(id);
		// 	if (existingPedido == null)
		// 	{
		// 		return NotFound();
		// 	}

		// 	pedidoDTO.Id = id;
		// 	var updatedPedido = await _service.UpdatePedidoAsync(pedidoDTO);
		// 	return Ok(updatedPedido);
		// }


		// PUT: api/pedidos/{id}/cancelar
		[HttpPut("{id}/cancelar")]
		public async Task<IActionResult> Cancelar(Guid id)
		{
			var pedido = await _service.GetPedidoByIdAsync(id);

			if (pedido == null)
			{
				return NotFound();
			}

			pedido.Status = PedidoStatus.CANCELADO;
			await _service.UpdatePedidoAsync(pedido);

			return Ok(pedido);
		}

		// PUT: api/pedidos/{id}/callback
		[HttpPut("{id}/callback")]
		public async Task<IActionResult> UpdateStatus(Guid id)
		{
			var pedido = await _service.GetPedidoByIdAsync(id);

			if (pedido == null)
			{
				return NotFound();
			}

			pedido.Status = PedidoStatus.PREPARANDO;
			await _service.UpdatePedidoAsync(pedido);

			return Ok(pedido);
		}

		// PUT: api/pedidos/{id}/preparo-finalizado
		[HttpPut("{id}/preparo-finalizado")]
		public async Task<IActionResult> PreparoFinalizado(Guid id)
		{
			var pedido = await _service.GetPedidoByIdAsync(id);

			if (pedido == null)
			{
				return NotFound();
			}

			pedido.Status = PedidoStatus.CONCLUIDO;
			await _service.UpdatePedidoAsync(pedido);

			return Ok(pedido);
		}

		// PUT: api/pedidos/{id}/entregue
		[HttpPut("{id}/entregue")]
		public async Task<IActionResult> Entregue(Guid id)
		{
			var pedido = await _service.GetPedidoByIdAsync(id);

			if (pedido == null)
			{
				return NotFound();
			}

			pedido.Status = PedidoStatus.ENTREGUE;
			await _service.UpdatePedidoAsync(pedido);

			return Ok(pedido);
		}

		// DELETE: api/pedidos/{id}
		[HttpDelete("{id}/deletar")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _service.DeletePedidoAsync(id);
			if (!result)
			{
				return NotFound();
			}

			return NoContent();
		}


	}
}
