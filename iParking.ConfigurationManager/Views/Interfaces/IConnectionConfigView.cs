using iParkingv8.Object.ConfigObjects.AppConfigs;
using iParkingv8.Object.ConfigObjects.OEMConfigs;
using iParkingv8.Object.Objects.Payments;
using Kztek.Object;
using Kztek.Object.Entity.ConfigObjects;
using ServerConfig = iParkingv8.Object.Objects.Systems.ServerConfig;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IConnectionConfigView
    {
        // Sự kiện người dùng
        event EventHandler ConfirmClicked;
        event EventHandler CancelClicked;

        // Thuộc tính liên quan tới từng tab cấu hình
        ServerConfig? ServerConfig { get; set; }
        RabbitMQConfig? RabbitMQConfig { get; set; }
        MQTTConfig? MqttConfig { get; set; }

        LprConfig? LprConfig { get; set; }
        AppOption? AppOption { get; set; }
        OEMConfig? OemConfig { get; set; }
        ThirdPartyConfig? ThirdPartyConfig { get; set; }
        PaymentKioskConfig? PaymentConfig { get; set; }

        // Phương thức hiển thị/hỗ trợ UI
        void ShowMessage(string message, string title);
        void Close();
    }

}
