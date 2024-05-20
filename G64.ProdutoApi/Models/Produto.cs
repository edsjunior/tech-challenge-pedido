namespace G64.ProdutoApi.Models;

public class Produto
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public decimal Preco { get; set; }
    public string? Descricao { get; set; }
    public ICollection<Ingrediente>? Ingredientes { get; set; }

    public ICollection<Combo>? Combos { get; set; }

    public tipo Tipo { get; set; }

}
public enum tipo
{
    LANCHE,
    BEBIDA,
    ACOMPANHAMENTO
}

