using G64.ProdutoApi.DTOs;

namespace G64.ProdutoApi.Services;

public interface IIngredienteService
{
    Task<IEnumerable<IngredienteDTO>> GetIngredientes();

    Task<IngredienteDTO> GetIngredienteById(Guid id);
    Task AddIngredienteDTO(IngredienteDTO ingredienteDTO);

    Task UpdateIngredienteDTO(IngredienteDTO ingredienteDTO);

    Task RemoveIngrediente(Guid id);
}
