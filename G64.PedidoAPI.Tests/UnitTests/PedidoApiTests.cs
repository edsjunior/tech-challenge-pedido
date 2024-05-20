using AutoMapper;
using G64.PedidoAPI.Context;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace G64.PedidoAPI.Tests.UnitTests;

public class PedidoApiTests
{
    private readonly ServiceProvider _serviceProvider;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly PedidoService _pedidoService;

    public PedidoApiTests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

        services.AddAutoMapper(cfg =>
        {
            cfg.CreateMap<Pedido, PedidoDTO>().ReverseMap();
            cfg.CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
        });

        services.AddScoped<ICarrinhoPedidoRepository, CarrinhoPedidoRepository>();
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

	/*[Fact]
	public async Task CreatePedido_ShouldAddPedido()
	{
		// Arrange
		var pedidoDto = new PedidoDTO
		{
			Data = DateTime.Now,
			Total = 100,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 25 },
			new ItemPedidoDTO { Descricao = "Item 2", Quantidade = 3, PrecoUnitario = 10 }
		},
			Status = PedidoStatus.PENDENTE
		};

		// Act
		var createdPedido = await _pedidoService.CreatePedidoAsync(pedidoDto);
		var result = _context.Pedidos.Include(p => p.Itens).FirstOrDefault(p => p.Id == createdPedido.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(pedidoDto.Total, result.Total);
		ClearDatabase();
	}*/

	[Fact]
	public async Task GetAllPedidos_ShouldReturnPedidos()
	{
		// Arrange
		await _pedidoService.CreatePedidoAsync(new PedidoDTO
		{
			Data = DateTime.Now,
			Total = 100,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 25 }
		},
			Status = PedidoStatus.PENDENTE
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
			Data = DateTime.Now,
			Total = 100,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 25 }
		},
			Status = PedidoStatus.PENDENTE
		});

		// Act
		var result = await _pedidoService.GetPedidoByIdAsync(createdPedido.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(createdPedido.Id, result.Id);
		ClearDatabase();
	}


	/*[Fact]
	public async Task UpdatePedido_ShouldModifyPedido()
	{
		// Arrange
		var createdPedido = await _pedidoService.CreatePedidoAsync(new PedidoDTO
		{
			Data = DateTime.Now,
			Total = 100,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 25 }
		},
			Status = PedidoStatus.PENDENTE
		});

		var updateDto = new PedidoDTO
		{
			Id = createdPedido.Id,
			Data = DateTime.Now,
			Total = 200,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 50 }
		},
			Status = PedidoStatus.CONCLUIDO
		};

		// Act
		var updatedPedido = await _pedidoService.UpdatePedidoAsync(updateDto);
		var result = await _pedidoService.GetPedidoByIdAsync(updatedPedido.Id);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(updateDto.Total, result.Total);
		Assert.Equal(PedidoStatus.CONCLUIDO, result.Status);
		ClearDatabase();
	}*/

	[Fact]
	public async Task DeletePedido_ShouldRemovePedido()
	{
		// Arrange
		var createdPedido = await _pedidoService.CreatePedidoAsync(new PedidoDTO
		{
			Data = DateTime.Now,
			Total = 100,
			Itens = new List<ItemPedidoDTO>
		{
			new ItemPedidoDTO { Descricao = "Item 1", Quantidade = 2, PrecoUnitario = 25 }
		},
			Status = PedidoStatus.PENDENTE
		});

		// Act
		var result = await _pedidoService.DeletePedidoAsync(createdPedido.Id);
		var deletedPedido = await _pedidoService.GetPedidoByIdAsync(createdPedido.Id);

		// Assert
		Assert.True(result);
		Assert.Null(deletedPedido);
		ClearDatabase();
	}


}