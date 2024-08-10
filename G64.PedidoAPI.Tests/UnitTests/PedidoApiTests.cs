using AutoMapper;
using G64.PedidoAPI.Context;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using System.Text.Json;
using Moq.Protected;
using Microsoft.AspNetCore.Mvc;
using G64.PedidoAPI.Controllers;

namespace G64.PedidoAPI.Tests.UnitTests;

public class PedidoApiTests
{
	private readonly ServiceProvider _serviceProvider;
	private readonly AppDbContext _context;
	private readonly IMapper _mapper;
	private readonly PedidoService _pedidoService;

	private readonly Mock<IPedidoRepository> _repositoryMock;
	private readonly Mock<IMapper> _mapperMock;
	private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
	private readonly IConfiguration _configuration;
	// private readonly PagamentoClient _pagamentoClient;

	public PedidoApiTests()
	{
		var services = new ServiceCollection();
		services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

		services.AddAutoMapper(cfg =>
		{
			cfg.CreateMap<Pedido, PedidoDTO>().ReverseMap();
			cfg.CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
		});

		services.AddScoped<IPedidoRepository, PedidoRepository>();
		services.AddScoped<PedidoService>();

		_serviceProvider = services.BuildServiceProvider();
		_context = _serviceProvider.GetRequiredService<AppDbContext>();
		_mapper = _serviceProvider.GetRequiredService<IMapper>();
		_pedidoService = _serviceProvider.GetRequiredService<PedidoService>();
	}

	private void ClearDatabase()
	{
		_context.Pedidos.RemoveRange(_context.Pedidos);
		_context.ItensPedidos.RemoveRange(_context.ItensPedidos);
		_context.SaveChanges();
	}

	[Fact]
	public async Task GetAllPedidos_ShouldReturnPedidos()
	{
		// Arrange
		await _pedidoService.CreatePedidoAsync(new PedidoDTO
		{
			data = DateTime.Now,
			valorTotal = 100,
			items = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { descricao = "Item 1", quantidade = 2, valorPorUnidade = 25 }
		},
			status = PedidoStatus.PENDENTE.ToString()
		});

		// Act
		var pedidos = await _pedidoService.GetAllPedidosAsync();

		// Assert
		Assert.NotEmpty(pedidos);
		ClearDatabase();
	}

	[Fact]
	public async Task GetPedidoById_ShouldReturnPedido()
	{
		// Arrange
		var createdPedido = await _pedidoService.CreatePedidoAsync(new PedidoDTO
		{
			data = DateTime.Now,
			valorTotal = 100,
			items = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { descricao = "Item 1", quantidade = 2, valorPorUnidade = 25 }
		},
			status = PedidoStatus.PENDENTE.ToString()
		}); ;

		// Act
		var result = await _pedidoService.GetPedidoByIdAsync(createdPedido.pedidoId);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(createdPedido.pedidoId, result.pedidoId);
		ClearDatabase();
	}

	//[Fact]
	//public async Task DeletePedido_ShouldCancelPedido()
	//{
	//	// Arrange
	//	var createdPedido = await _pedidoService.CreatePedidoAsync(new PedidoDTO
	//	{
	//		Data = DateTime.Now,
	//		Total = 100,
	//		Itens = new List<ItemPedidoDTO>
	//	{
	//		new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, ValorPorUnidade = 25 }
	//	},
	//		Status = PedidoStatus.PENDENTE
	//	});

	//	// Act
	//	var result = await _pedidoService.UpdatePedidoAsync(createdPedido);
	//	//var deletedPedido = await _pedidoService.GetPedidoByIdAsync(createdPedido.Id);

	//	// Assert
	//	Assert.Equal(result.Status, createdPedido.Status);
	//	//Assert.Null(deletedPedido);
	//	ClearDatabase();
	//}

}