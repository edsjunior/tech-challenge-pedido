using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.PedidoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarrinhoPedidoController : ControllerBase
	{
		private readonly ICarrinhoPedidoRepository _repository;

		public CarrinhoPedidoController(ICarrinhoPedidoRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("getCart/{id}")]
		public async Task<ActionResult<CarrinhoPedidoDTO>> GetByUserId(string id)
		{
			var carrinhoPedidoDTO = await _repository.GetCarrinhoPedidoByUserIdAsync(id);

			if(carrinhoPedidoDTO is null)
				return NotFound();

			return Ok(carrinhoPedidoDTO);
		}

		[HttpPost("addpedido")]
		public async Task<ActionResult<CarrinhoPedidoDTO>> AddPedido(CarrinhoPedidoDTO carrinhoPedidoDTO)
		{
			var carrinhoPedido = await _repository.UpdateCartAsync(carrinhoPedidoDTO);

            if (carrinhoPedido is null)
                return NotFound();

            return Ok(carrinhoPedido);

        }

        [HttpPut("updatepedido")]
        public async Task<ActionResult<CarrinhoPedidoDTO>> UpdatePedido(CarrinhoPedidoDTO carrinhoPedidoDTO)
        {
            var carrinhoPedido = await _repository.UpdateCartAsync(carrinhoPedidoDTO);

            if (carrinhoPedido is null)
                return NotFound();

            return Ok(carrinhoPedido);

        }

        [HttpDelete("deletepedido/{id}")]
        public async Task<ActionResult<CarrinhoPedidoDTO>> DeletePedido(Guid id)
        {
            var status = await _repository.DeleteItemCarrinhoAsync(id);

            if (!status)
                return BadRequest();

            return Ok(status);

        }
    }
}
