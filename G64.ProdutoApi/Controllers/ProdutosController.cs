using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.ProdutoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		private readonly IProdutoService _produtoService;

		public ProdutosController(IProdutoService produtoService)
		{
			_produtoService = produtoService;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
		{
			var produtosDTO = await _produtoService.GetProdutos();

			if(produtosDTO == null)
				return NotFound("Produto não encontrado");

			return Ok(produtosDTO);
		}

        [HttpGet("ingredientes")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosIngredientes()
        {
            var produtosDTO = await _produtoService.GetProdutosIngredientes();

            if (produtosDTO == null)
                return NotFound("Produto não encontrado");

            return Ok(produtosDTO);
        }

        [HttpGet("{id:Guid}", Name = "GetProduto")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get(Guid id)
        {
            var produtosDTO = await _produtoService.GetProdutoById(id);

            if (produtosDTO == null)
                return NotFound("Produto não encontrado");

            return Ok(produtosDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO == null)
                return BadRequest("Dados inválidos");

            await _produtoService.AddProdutoDTO(produtoDTO);

            return new CreatedAtRouteResult("GetProduto", new { id = produtoDTO.Id }, produtoDTO);
        }

        [HttpPut("id: Guid")]
        public async Task<ActionResult>Put(Guid id, [FromBody] ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
                return BadRequest();

            if (produtoDTO == null)
                return BadRequest();

            await _produtoService.UpdateProdutoDTO(produtoDTO); 

            return Ok(produtoDTO); 
        }

        [HttpDelete("id: Guid")]
        public async Task<ActionResult<ProdutoDTO>> Delete (Guid id)
        {
            var produtoDto = await _produtoService.GetProdutoById(id);
            if (produtoDto == null)
                return NotFound("Produto não encontrado");

            await _produtoService.RemoveProduto(id);

            return Ok(produtoDto);
        }
    }
}
