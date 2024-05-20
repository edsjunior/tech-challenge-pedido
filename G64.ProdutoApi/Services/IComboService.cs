using G64.ProdutoApi.DTOs;

namespace G64.ProdutoApi.Services;

public interface IComboService
{
    Task<IEnumerable<ComboDTO>> GetCombos();

    Task<IEnumerable<ComboDTO>> GetComboProdutos();

    Task<ComboDTO> GetComboById(Guid id);
    Task AddComboTO(ComboDTO comboDTO);

    Task UpdateComboDTO(ComboDTO comboDTO);

    Task RemoveComboDTO(Guid id);
}
