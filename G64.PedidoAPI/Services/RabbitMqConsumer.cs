using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using G64.PedidoAPI.DTOs;
using G64.PedidoAPI.Models;

namespace G64.PedidoAPI.Services
{
    public class RabbitMqConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            // Configuração da conexão com RabbitMQ
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq", // Nome do host do RabbitMQ no Docker Compose
                UserName = "guest",     // Usuário do RabbitMQ
                Password = "guest"  // Senha do RabbitMQ
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "pagamentoQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Processar mensagem de pagamento
                await ProcessPaymentMessageAsync(message);
            };

            _channel.BasicConsume(queue: "pagamentoQueue", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessPaymentMessageAsync(string message)
        {
			
			// Deserializar a mensagem de pagamento
			var pagamentoResponseDto = JsonConvert.DeserializeObject<PagamentoResponseDTO>(message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var pedidoService = scope.ServiceProvider.GetRequiredService<PedidoService>();
                if(pagamentoResponseDto.pedidoId != null)
                {
					var pedido = await pedidoService.GetPedidoByIdAsync(Guid.Parse(pagamentoResponseDto.pedidoId));

					if (pedido != null)
					{
						// Atualizar status do pedido para "PREPARANDO"
						pedido.statusPagamento = pagamentoResponseDto.status;
						if (pedido.statusPagamento == PagamentoStatus.APROVADO.ToString())
							pedido.status = PedidoStatus.PREPARANDO.ToString();
						else
							pedido.status = PedidoStatus.CANCELADO.ToString();

						await pedidoService.UpdatePedidoAsync(pedido);
					}
				}
            }
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
