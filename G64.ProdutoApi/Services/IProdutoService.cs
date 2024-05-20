using G64.ProdutoApi.DTOs;

namespace G64.ProdutoApi.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoDTO>> GetProdutos();

    Task<IEnumerable<ProdutoDTO>> GetProdutosIngredientes();

    Task<ProdutoDTO> GetProdutoById(Guid id);
    Task AddProdutoDTO(ProdutoDTO produtoDTO);

    Task UpdateProdutoDTO(ProdutoDTO produtoDTO);

    Task RemoveProduto(Guid id);
}
