using System.ComponentModel.DataAnnotations;

namespace G64.ProdutoApi.DTOs;

public class IngredienteDTO
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Descrição obrigatória")]
    [MinLength(3)]
    [MaxLength(255)]
    public string? Descricao { get; set; }
}
