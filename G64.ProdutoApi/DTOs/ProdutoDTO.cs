using G64.ProdutoApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace G64.ProdutoApi.DTOs
{
	public class ProdutoDTO
	{
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Nome obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Preço obrigatório")]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "Descrição obrigatória")]
        [MinLength(3)]
        [MaxLength(255)]
        public string? Descricao { get; set; }

        [JsonIgnore]
        public ICollection<Ingrediente>? Ingredientes { get; set; }

        [JsonIgnore]
        public ICollection<Combo>? Combos { get; set; }

        [Required(ErrorMessage = "Tipo obrigatório")]
        public tipo Tipo { get; set; }
    }
}
