using G64.ProdutoApi;
using G64.ProdutoApi.Models;
using System.Text.Json.Serialization;
namespace G64.ProdutoApi.DTOs;


public class ComboDTO
{
    public Guid Id { get; set; }
    
    public ICollection<Produto>? Produtos { get; set; }
}
