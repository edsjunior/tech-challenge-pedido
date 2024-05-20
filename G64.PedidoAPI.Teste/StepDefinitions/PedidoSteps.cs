using TechTalk.SpecFlow;
using NUnit.Framework;
using G64.PedidoAPI.Models;
using G64.PedidoAPI.Repositories;
using G64.PedidoAPI.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using G64.PedidoAPI.DTOs;


namespace G64.PedidoAPI.Teste.StepDefinitions
{
    [Binding]
    public class PedidoSteps
    {
        private CarrinhoPedidoDTO _carrinhoPedido;
        private CarrinhoPedidoRepository _repository;

        public PedidoSteps()
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<CarrinhoPedido, CarrinhoPedido>();
                cfg.CreateMap<ItemPedido, ItemPedido>();
            });

            var serviceProvider = services.BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var mapper = serviceProvider.GetRequiredService<IMapper>();

            _repository = new CarrinhoPedidoRepository(context, mapper);
        }

        [Given(@"que eu tenho um novo pedido")]
        public void DadoQueEuTenhoUmNovoPedido()
        {
            _carrinhoPedido = new CarrinhoPedidoDTO()
            {
                HeaderPedido = new HeaderPedidoDTO { Id = Guid.NewGuid(), Status = 0, UserId = "00000000191" },
                ItemPedidos = new List<ItemPedidoDTO>() { new ItemPedidoDTO
            {
               Id = Guid.NewGuid(),
               ProdutoId = Guid.NewGuid(),
               Quantidade = 1,
               HeaderPedidoId = Guid.NewGuid(),
               Produto = new ProdutoDTO
               {
                   Id = Guid.NewGuid(),  Nome = "X-Bacon", Preco = 20, Descricao = "Lanche com bacon"
               }
            }
            }
            };
        }

        [When(@"eu criar o pedido")]
        public void QuandoEuCriarOPedido()
        {
            _repository.UpdateCarrinhoPedidoAsync(_carrinhoPedido);
        }

        [Then(@"o pedido deve ser criado com sucesso")]
        public void EntaoOPedidoDeveSerCriadoComSucesso()
        {
            var createdPedido = _repository.GetCarrinhoPedidoById(_carrinhoPedido.HeaderPedido.Id);
            Assert.NotNull(createdPedido);
            Assert.AreEqual(_carrinhoPedido.HeaderPedido.Id, createdPedido.Id);
        }
    }
}
