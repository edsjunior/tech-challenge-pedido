using G64.ProdutoApi.DTOs;
using G64.ProdutoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace G64.ProdutoApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CombosController : ControllerBase
	{
        private readonly IComboService _comboService;

        public CombosController(IComboService comboService)
        {
            _comboService = comboService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComboDTO>>> Get()
        {
            var combosDto = await _comboService.GetCombos();

            if (combosDto == null)
                return NotFound("Combos não encontrados");

            return Ok(combosDto);
        }

        [HttpGet("combo")]
        public async Task<ActionResult<IEnumerable<ComboDTO>>> GetCombosProdutos()
        {
            var combosDTO = await _comboService.GetComboProdutos();

            if (combosDTO == null)
                return NotFound("Combo não encontrado");

            return Ok(combosDTO);
        }

        [HttpGet("{id:Guid}", Name = "GetCombo")]
        public async Task<ActionResult<IEnumerable<ComboDTO>>> Get(Guid id)
        {
            var comboDto = await _comboService.GetComboById(id);

            if (comboDto == null)
                return NotFound("Combo não encontrado");

            return Ok(comboDto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ComboDTO comboDTO)
        {
            if (comboDTO == null)
                return BadRequest("Dados inválidos");

            await _comboService.AddComboTO(comboDTO);

            return new CreatedAtRouteResult("GetCombo", new { id = comboDTO.Id }, comboDTO);
        }

        [HttpPut("id: Guid")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ComboDTO comboDTO)
        {
            if (id != comboDTO.Id)
                return BadRequest();

            if (comboDTO == null)
                return BadRequest();

            await _comboService.UpdateComboDTO(comboDTO);

            return Ok(comboDTO);
        }

        [HttpDelete("id: Guid")]
        public async Task<ActionResult<ComboDTO>> Delete(Guid id)
        {
            var comboDto = await _comboService.GetComboById(id);
            if (comboDto == null)
                return NotFound("Combo não encontrado");

            await _comboService.RemoveComboDTO(id);

            return Ok(comboDto);
        }
    }
}
