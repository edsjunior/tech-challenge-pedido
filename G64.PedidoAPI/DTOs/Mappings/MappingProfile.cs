using AutoMapper;
using G64.PedidoAPI.Models;
using System.Reflection.PortableExecutable;

namespace G64.PedidoAPI.DTOs.Mappings
{
	public class MappingProfile: Profile
    {
        public MappingProfile()
        {
			CreateMap<CarrinhoPedidoDTO, CarrinhoPedido>().ReverseMap();
            CreateMap<HeaderPedidoDTO, HeaderPedido>().ReverseMap();
            CreateMap<ItemPedidoDTO, ItemPedido>().ReverseMap();
            CreateMap<ProdutoDTO, Produto>().ReverseMap();
        }
    }
}
