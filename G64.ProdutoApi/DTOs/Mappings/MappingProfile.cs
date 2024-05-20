using AutoMapper;
using G64.ProdutoApi.Models;
namespace G64.ProdutoApi.DTOs.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Ingrediente, IngredienteDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().ReverseMap();
        CreateMap<Combo, ComboDTO>().ReverseMap();
    }
}
