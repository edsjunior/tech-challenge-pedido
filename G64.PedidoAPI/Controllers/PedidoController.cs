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

		public PedidoController(PedidoService service, PagamentoService pagamentoService)
		{
			_service = service;
			_pagamentoService = pagamentoService;
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
			pedidoDTO.statusPagamento = PagamentoStatus.PENDENTE.ToString();
			var createdPedido = await _service.CreatePedidoAsync(pedidoDTO);

			var items = createdPedido.items.Select(item => new ItemPedidoDTO
			{
				uuid = item.uuid,
				categoria = item.categoria,
				titulo = item.titulo,
				descricao = item.descricao,
				quantidade = item.quantidade,
				valorPorUnidade = item.valorPorUnidade
			}).ToList();

			// Criar o objeto de requisição para o pagamento
			var pagamentoRequest = new PagamentoRequestDTO
			{
				pedidoId = createdPedido.pedidoId.ToString(),
				valorTotal = createdPedido.valorTotal,
				items = items
			};


			// Faz a chamada à API de pagamento
			var pagamentoResponse = await _pagamentoService.CriaPagamentoAsync(pagamentoRequest);

			// Verifique a resposta do pagamento e manipule o resultado conforme necessário
			if (pagamentoResponse == null)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, "Falha ao processar o pagamento");
			}

			// Atualiza o pedido com informações de pagamento
			createdPedido.statusPagamento = pagamentoResponse.status;
			await _service.UpdatePedidoAsync(createdPedido);

			return CreatedAtAction(nameof(GetById), new { id = createdPedido.pedidoId }, createdPedido);
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

			pedido.status = PedidoStatus.CANCELADO.ToString();
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

			pedido.status = PedidoStatus.PREPARANDO.ToString();
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

			pedido.status = PedidoStatus.CONCLUIDO.ToString();
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

			pedido.status = PedidoStatus.ENTREGUE.ToString();
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
