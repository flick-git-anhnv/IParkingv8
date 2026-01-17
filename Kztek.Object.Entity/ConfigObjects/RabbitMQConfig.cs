namespace Kztek.Object
{
    public class RabbitMQConfig
    {
        //Thông tin Rabbit MQ Server
        public string RabbitMqUrl { get; set; } = string.Empty;
        public string RabbitMqUsername { get; set; } = string.Empty;
        public string RabbitMqPassword { get; set; } = string.Empty;
        public string RabbitMQExchangeName { get; set; } = string.Empty;
        public string RabbitMQRoutingKey { get; set; } = string.Empty;
        public int Port { get; set; }
    }
}
