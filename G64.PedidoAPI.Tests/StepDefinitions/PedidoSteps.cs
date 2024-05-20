using G64.PedidoAPI.Controllers;
using G64.PedidoAPI.Context;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace G64.PedidoAPI.Tests.StepDefinitions
{
	[Binding]
	public class PedidoSteps
	{
		private readonly ScenarioContext _scenarioContext;
		private ServiceProvider _serviceProvider;
		private PedidoDTO _pedidoDto;
		private ActionResult<PedidoDTO> _actionResult;

		public PedidoSteps(ScenarioContext scenarioContext)
		{
			_scenarioContext = scenarioContext;
			var services = new ServiceCollection();
			services.AddDbContext<AppDbContext>(options =>
				options.UseInMemoryDatabase("TestBdd"));

			// Configure AutoMapper directly here
			services.AddAutoMapper(cfg =>
			{
				cfg.CreateMap<Pedido, PedidoDTO>().ReverseMap();
				cfg.CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
			});


			services.AddScoped<ICarrinhoPedidoRepository, CarrinhoPedidoRepository>();
			services.AddScoped<PedidoService>();
			services.AddScoped<CarrinhoPedidoController>();

			_serviceProvider = services.BuildServiceProvider();

		}

		[Given(@"I have a new pedido")]
		public void GivenIHaveANewPedido()
		{
			_pedidoDto = new PedidoDTO
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

		}

		[When(@"I create the pedido")]
		public async Task WhenICreateThePedido()
		{
			var controller = _serviceProvider.GetRequiredService<CarrinhoPedidoController>();
			_actionResult = await controller.Create(_pedidoDto);
		}

		[Then(@"the pedido should be created successfully")]
		public void ThenThePedidoShouldBeCreatedSuccessfully()
		{
			var createdResult = _actionResult.Result as CreatedAtActionResult;
			Assert.NotNull(createdResult);
			var createdPedido = createdResult.Value as PedidoDTO;
			Assert.NotNull(createdPedido);
			Assert.Equal(_pedidoDto.Total, createdPedido.Total);
		}
	}
}
