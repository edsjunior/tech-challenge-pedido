using AutoMapper;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.PedidoAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CarrinhoPedidoController : ControllerBase
	{
		private readonly PedidoService _service;

		public CarrinhoPedidoController(PedidoService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PedidoDTO>>> GetAll()
		{
			var pedidos = await _service.GetAllPedidosAsync();
			return Ok(pedidos);
		}

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

		[HttpPost]
		public async Task<ActionResult<PedidoDTO>> Create([FromBody] PedidoDTO pedidoDTO)
		{
			var createdPedido = await _service.CreatePedidoAsync(pedidoDTO);
			return CreatedAtAction(nameof(GetById), new { id = createdPedido.Id }, createdPedido);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] PedidoDTO pedidoDTO)
		{
			var existingPedido = await _service.GetPedidoByIdAsync(id);
			if (existingPedido == null)
			{
				return NotFound();
			}

			pedidoDTO.Id = id;
			var updatedPedido = await _service.UpdatePedidoAsync(pedidoDTO);
			return Ok(updatedPedido);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _service.DeletePedidoAsync(id);
			if (!result)
			{
				return NotFound();
			}
			return NoContent();
		}

		[HttpPut("{id}/status")]
		public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] PedidoStatus status)
		{
			var updatedPedido = await _service.UpdatePedidoStatusAsync(id, status);
			if (updatedPedido == null)
			{
				return NotFound();
			}

			return Ok(updatedPedido);
		}
	}
}
