using Api.Transferencia.Configurations;
using Api.Transferencia.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Api.Transferencia.Services
{
    public class PublishMessage : IPublishMessage
    {
        private readonly ConnectionFactory _factory;
        private readonly RabbitMqConfiguration _config;
        public PublishMessage(IOptions<RabbitMqConfiguration> options)
        {
            _config = options.Value;

            _factory = new ConnectionFactory
            {
                HostName = _config.Host
            };
        }
        /// <summary>
        /// Método responsável por publicar mensagem na fila
        /// </summary>
        /// <param name="message">Dados da Mensagem</param>
        /// <returns></returns>
       public async Task<ResultPublish> SendMessageAsync(TransferInputDto message)
        {            
            ResultPublish result = new ResultPublish();

            try
            {
                using (var connection = _factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: _config.Queue,
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

                        var stringfiedMessage = JsonConvert.SerializeObject(message);
                        var bytesMessage = Encoding.UTF8.GetBytes(stringfiedMessage);

                        channel.BasicPublish(
                            exchange: _config.Exchange,
                            routingKey: _config.Exchange,
                            basicProperties: null,
                            body: bytesMessage);
                    }
                }                
            }
            catch (Exception)
            {
                result.SuccessfullyExecuted = false;
                result.Message = "Erro ao tentar publicar Mensagem"; 
                return result;
            }
            
            result.SuccessfullyExecuted = true;
            result.Message = "Mensagem enviada para fila com sucesso!";
            await Task.Yield(); //Resolver warning de métodos assíncronos 
            return result;
        }
    }
}
