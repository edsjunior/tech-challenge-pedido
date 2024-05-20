using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.ProdutoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IngredientesController : ControllerBase
	{
        private readonly IIngredienteService _ingredienteService;

        public IngredientesController(IIngredienteService ingredienteService)
        {
            _ingredienteService = ingredienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredienteDTO>>> Get()
        {
            var ingredientesDto = await _ingredienteService.GetIngredientes();

            if (ingredientesDto == null)
                return NotFound("Ingrediente não encontrado");

            return Ok(ingredientesDto);
        }

        [HttpGet("{id:Guid}", Name = "GetIngrediente")]
        public async Task<ActionResult<IEnumerable<IngredienteDTO>>> Get(Guid id)
        {
            var ingredienteDto = await _ingredienteService.GetIngredienteById(id);

            if (ingredienteDto == null)
                return NotFound("Ingrediente não encontrado");

            return Ok(ingredienteDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] IngredienteDTO ingredienteDTO)
        {
            if (ingredienteDTO == null)
                return BadRequest("Dados inválidos");

            await _ingredienteService.AddIngredienteDTO(ingredienteDTO);

            return new CreatedAtRouteResult("GetIngrediente", new { id = ingredienteDTO.Id }, ingredienteDTO);
        }

        [HttpPut("id: Guid")]
        public async Task<ActionResult> Put(Guid id, [FromBody] IngredienteDTO ingredienteDTO)
        {
            if (id != ingredienteDTO.Id)
                return BadRequest();

            if (ingredienteDTO == null)
                return BadRequest();

            await _ingredienteService.UpdateIngredienteDTO(ingredienteDTO);

            return Ok(ingredienteDTO);
        }

        [HttpDelete("id: Guid")]
        public async Task<ActionResult<IngredienteDTO>> Delete(Guid id)
        {
            var ingredienteDto = await _ingredienteService.GetIngredienteById(id);
            if (ingredienteDto == null)
                return NotFound("Ingrediente não encontrado");

            await _ingredienteService.RemoveIngrediente(id);

            return Ok(ingredienteDto);
        }
    }
}
