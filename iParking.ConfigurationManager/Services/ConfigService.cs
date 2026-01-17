using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using Kztek.Tool;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Services
{
    public class ConfigService : IConfigService
    {
        //-- Server Config
        public ServerConfig? LoadServerConfig()
        {
            return NewtonSoftHelper<ServerConfig>.DeserializeObjectFromPath(IparkingingPathManagement.serverConfigPath);
        }
        public RabbitMQConfig? LoadRabbitMQConfig()
        {
            return NewtonSoftHelper<RabbitMQConfig>.DeserializeObjectFromPath(IparkingingPathManagement.rabbitmqConfigPath);
        }
        public MQTTConfig? LoadMqttConfig()
        {
            return NewtonSoftHelper<MQTTConfig>.DeserializeObjectFromPath(IparkingingPathManagement.mqttConfigPath);
        }

        public bool SaveServerConfig(ServerConfig? config)
        {
            return NewtonSoftHelper<ServerConfig>.SaveConfig(config, IparkingingPathManagement.serverConfigPath);
        }
        public bool SaveRabbitMQConfig(RabbitMQConfig? config)
        {
            return NewtonSoftHelper<RabbitMQConfig>.SaveConfig(config, IparkingingPathManagement.rabbitmqConfigPath);
        }
        public bool SaveMqttConfig(MQTTConfig? config)
        {
            return NewtonSoftHelper<MQTTConfig>.SaveConfig(config, IparkingingPathManagement.mqttConfigPath);
        }

        //-- LPR Config
        public LprConfig? LoadLprConfig()
        {
            return NewtonSoftHelper<LprConfig>.DeserializeObjectFromPath(IparkingingPathManagement.lprConfigPath);
        }
        public bool SaveLprConfig(LprConfig? config)
        {
            return NewtonSoftHelper<LprConfig>.SaveConfig(config, IparkingingPathManagement.lprConfigPath);
        }

        //-- App Option
        public AppOption? LoadAppOption()
        {
            return NewtonSoftHelper<AppOption>.DeserializeObjectFromPath(IparkingingPathManagement.appOptionConfigPath);
        }
        public bool SaveAppOption(AppOption? config)
        {
            return NewtonSoftHelper<AppOption>.SaveConfig(config, IparkingingPathManagement.appOptionConfigPath);
        }

        //-- OEM Config
        public OEMConfig? LoadOemConfig()
        {
            return NewtonSoftHelper<OEMConfig>.DeserializeObjectFromPath(IparkingingPathManagement.oemConfigPath);
        }
        public bool SaveOemConfig(OEMConfig? config)
        {
            return NewtonSoftHelper<OEMConfig>.SaveConfig(config, IparkingingPathManagement.oemConfigPath);
        }

        //-- Payment Config
        public PaymentKioskConfig? LoadPaymentConfig()
        {
            return NewtonSoftHelper<PaymentKioskConfig>.DeserializeObjectFromPath(IparkingingPathManagement.paymentKioskConfigPath);
        }
        public bool SavePaymentConfig(PaymentKioskConfig? config)
        {
            return NewtonSoftHelper<PaymentKioskConfig>.SaveConfig(config, IparkingingPathManagement.paymentKioskConfigPath);
        }

        //-- Third Party
        public ThirdPartyConfig? LoadThirdPartyConfig()
        {
            return NewtonSoftHelper<ThirdPartyConfig>.DeserializeObjectFromPath(IparkingingPathManagement.thirtPartyConfigPath);
        }
        public bool SaveThirdPartyConfig(ThirdPartyConfig? config)
        {
            return NewtonSoftHelper<ThirdPartyConfig>.SaveConfig(config, IparkingingPathManagement.thirtPartyConfigPath);
        }
    }
}
