using G64.ProdutoApi.Models;

namespace G64.ProdutoApi.Repositories;

public interface IComboRepository
{
    Task<IEnumerable<Combo>> GetAll();
    Task<IEnumerable<Combo>> GetComboProdutos();
    Task<Combo> GetById(Guid id);
    Task<Combo> Create(Combo combo);
    Task<Combo> Update(Combo combo);
    Task<Combo> Delete(Guid id);
}
