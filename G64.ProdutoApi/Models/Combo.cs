namespace G64.ProdutoApi.Models;

public class Combo
{
    public Guid Id { get; set; }

    public ICollection<Produto>? Produtos { get; set; }

    /*public Combo(Guid id, ICollection<Produto> produtos)
    {
        Id = id;
        Produtos = produtos;
    }

    public decimal ValorTotal()
    {
        return Produtos.Sum(p => p.Preco);
    }*/

}
