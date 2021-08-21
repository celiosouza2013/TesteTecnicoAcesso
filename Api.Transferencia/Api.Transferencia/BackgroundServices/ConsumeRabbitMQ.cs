using Api.Transferencia.Configurations;
using Api.Transferencia.Models;
using Api.Transferencia.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Transferencia.BackgroundServices
{
    /// <summary>
    /// Classe responsável para criar serviço em segundo plano
    /// </summary>
    public class ConsumeRabbitMQ : BackgroundService
    {
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;        

        public ConsumeRabbitMQ(IOptions<RabbitMqConfiguration> option, 
                               IServiceProvider serviceProvider)
        {
            _configuration = option.Value;
            _serviceProvider = serviceProvider;            

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(
                            queue: _configuration.Queue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
            }
            catch (Exception)
            {
                throw;
            }
           
        }
        /// <summary>
        /// Método para consumo de item de de fila 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (sender, eventArgs) =>
                {
                    var contentArray = eventArgs.Body.ToArray();
                    var contentString = Encoding.UTF8.GetString(contentArray);
                    var _dataTransfer = JsonConvert.DeserializeObject<TransferOutput>(contentString);

                    NotifyTransference(_dataTransfer);

                    _channel.BasicAck(eventArgs.DeliveryTag, false);
                };

                _channel.BasicConsume(_configuration.Queue, false, consumer);

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public void NotifyTransference(TransferOutput _dataTransfer)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                notificationService.NotifyTransference(_dataTransfer);
            }
        }

        //TODO - Resiliência da conexão
        
        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}

