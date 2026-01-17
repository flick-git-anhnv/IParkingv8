using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Services
{
    public interface IConfigService
    {
        ServerConfig? LoadServerConfig();
        MQTTConfig? LoadMqttConfig();
        RabbitMQConfig? LoadRabbitMQConfig();

        LprConfig? LoadLprConfig();
        AppOption? LoadAppOption();
        OEMConfig? LoadOemConfig();
        ThirdPartyConfig? LoadThirdPartyConfig();
        PaymentKioskConfig? LoadPaymentConfig();

        bool SaveServerConfig(ServerConfig? config);
        bool SaveMqttConfig(MQTTConfig? config);
        bool SaveRabbitMQConfig(RabbitMQConfig? config);

        bool SaveLprConfig(LprConfig? config);
        bool SaveAppOption(AppOption? config);
        bool SaveOemConfig(OEMConfig? config);
        bool SaveThirdPartyConfig(ThirdPartyConfig? config);
        bool SavePaymentConfig(PaymentKioskConfig? config);
    }
}
