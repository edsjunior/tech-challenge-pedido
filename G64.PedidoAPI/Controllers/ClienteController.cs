using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace G64.PedidoAPI.Controllers
{
	[Route("api/clientes")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteService _service;

		public ClienteController(IClienteService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
		{
			var clientes = await _service.GetAllClientesAsync();
			return Ok(clientes);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ClienteDTO>> GetById(Guid id)
		{
			var cliente = await _service.GetClienteByIdAsync(id);
			if (cliente == null)
			{
				return NotFound();
			}
			return Ok(cliente);
		}

		[HttpPost]
		public async Task<ActionResult<ClienteDTO>> Create([FromBody] ClienteDTO clienteDTO)
		{
			var createdCliente = await _service.CreateClienteAsync(clienteDTO);
			return CreatedAtAction(nameof(GetById), new { id = createdCliente.ClienteId }, createdCliente);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] ClienteDTO clienteDTO)
		{
			var existingCliente = await _service.GetClienteByIdAsync(id);
			if (existingCliente == null)
			{
				return NotFound();
			}

			clienteDTO.ClienteId = id;
			var updatedCliente = await _service.UpdateClienteAsync(clienteDTO);
			return Ok(updatedCliente);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var existingCliente = await _service.GetClienteByIdAsync(id);
			if (existingCliente == null)
			{
				return NotFound();
			}

			await _service.DeleteClienteAsync(id);
			return NoContent();
		}
	}
}
