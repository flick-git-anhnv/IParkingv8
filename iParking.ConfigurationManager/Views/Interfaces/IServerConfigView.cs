using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IServerConfigView
    {
        ServerConfig GetServerConfig();
        RabbitMQConfig GetRabbitMQConfig();
        MQTTConfig GetMQTTConfig();

        void SetServerConfig(ServerConfig? config);
        void SetRabbitMQConfig(RabbitMQConfig? config);
        void SetMQTTConfig(MQTTConfig? config);
    }
}
