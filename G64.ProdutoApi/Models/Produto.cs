namespace G64.ProdutoApi.Models;

	public class Produto
	{
    public Guid Id { get; private set; }
    public string? Nome { get; private set; }
    public decimal Preco { get; private set; }
    public string? Descricao { get; set; }
    public IList<Ingrediente> Ingredientes { get; private set; }

    private Produto(Guid id, string nome, decimal preco, IList<Ingrediente> ingredientes)
    {
        Id = id;
        Nome = nome;
        Preco = preco;
        Ingredientes = ingredientes;
    }

    public static Produto CriaProduto(Guid id, string nome, decimal preco, IList<Ingrediente> ingredientes)
    {
        ValidaNome(nome);
        ValidaPreco(preco);
        return new Produto(id, nome, preco, ingredientes);
    }

    private static void ValidaNome(string nome)
    {
        if (string.IsNullOrEmpty(nome))
        {
            throw new ArgumentException("Nome deve estar preenchido");
        }
    }

    private static void ValidaPreco(decimal preco)
    {
        if (preco <= 0)
        {
            throw new ArgumentException("Preco deve ser maior que zero");
        }
    }

}
