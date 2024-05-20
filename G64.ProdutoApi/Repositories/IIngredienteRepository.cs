using G64.ProdutoApi.Models;

namespace G64.ProdutoApi.Repositories;

public interface IIngredienteRepository
{
    Task<IEnumerable<Ingrediente>> GetAll();
    Task<Ingrediente> GetById(Guid id);
    Task<Ingrediente> Create(Ingrediente ingrediente);
    Task<Ingrediente> Update(Ingrediente ingrediente);
    Task<Ingrediente> Delete(Guid id);
}
