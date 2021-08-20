namespace Api.Transferencia.Configurations
{    
    /// <summary>
    /// Configurações básicas para conexão com fila RabbitMQ
    /// </summary>
    public class RabbitMqConfiguration
    {
        public string Host { get; set; }
        public string Queue { get; set; }
        public string Exchange { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
