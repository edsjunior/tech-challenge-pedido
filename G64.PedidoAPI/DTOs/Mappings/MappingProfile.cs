using AutoMapper;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.DTOs;

namespace G64.PedidoAPI.DTOs.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Pedido, PedidoDTO>().ReverseMap();
			CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
		}
	}
}
