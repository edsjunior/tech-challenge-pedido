using G64.ProdutoApi.Models;

namespace G64.ProdutoApi.Repositories;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> GetAll();
    Task<IEnumerable<Produto>> GetProdutosIngredientes();
    Task<Produto> GetById(Guid id);
    Task<Produto> Create(Produto produto);
    Task<Produto> Update(Produto produto);
    Task<Produto> Delete(Guid id);
}
