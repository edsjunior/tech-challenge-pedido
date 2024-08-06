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
                UserName = "user",     // Usuário do RabbitMQ
                Password = "password"  // Senha do RabbitMQ
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: "pagamentos", durable: true, exclusive: false, autoDelete: false, arguments: null);
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

            _channel.BasicConsume(queue: "pagamentos", autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }

        private async Task ProcessPaymentMessageAsync(string message)
        {
            // Deserializar a mensagem de pagamento
            var pagamentoDto = JsonConvert.DeserializeObject<PagamentoDTO>(message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var pedidoService = scope.ServiceProvider.GetRequiredService<PedidoService>();
                var pedido = await pedidoService.GetPedidoByIdAsync(pagamentoDto.PedidoId);

                if (pedido != null)
                {
                    // Atualizar status do pedido para "PREPARANDO"
                    pedido.Status = PedidoStatus.PREPARANDO;
                    pedido.MetodoPagamento = pagamentoDto.MetodoPagamento;
                    await pedidoService.UpdatePedidoAsync(pedido);
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
