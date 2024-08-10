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


			services.AddScoped<IPedidoRepository, PedidoRepository>();
			services.AddScoped<PedidoService>();
			services.AddScoped<PedidoController>();

			_serviceProvider = services.BuildServiceProvider();

		}

		[Given(@"I have a new pedido")]
		public void GivenIHaveANewPedido()
		{
			_pedidoDto = new PedidoDTO
			{
				data = DateTime.Now,
				valorTotal = 100,
				items = new List<ItemPedidoDTO>
				{
					new ItemPedidoDTO { descricao = "Item 1", quantidade = 2, valorPorUnidade = 25 },
					new ItemPedidoDTO { descricao = "Item 2", quantidade = 3, valorPorUnidade = 10 }
				},
				status = PedidoStatus.PENDENTE.ToString()
			};

		}

		[When(@"I create the pedido")]
		public async Task WhenICreateThePedido()
		{
			var controller = _serviceProvider.GetRequiredService<PedidoController>();
			_actionResult = await controller.Create(_pedidoDto);
		}

		[Then(@"the pedido should be created successfully")]
		public void ThenThePedidoShouldBeCreatedSuccessfully()
		{
			var createdResult = _actionResult.Result as CreatedAtActionResult;
			Assert.NotNull(createdResult);
			var createdPedido = createdResult.Value as PedidoDTO;
			Assert.NotNull(createdPedido);
			Assert.Equal(_pedidoDto.valorTotal, createdPedido.valorTotal);
		}
	}
}
