using iParkingv8.Object.ConfigObjects.OEMConfigs;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IThirdPartyView
    {
        ThirdPartyConfig? GetConfig();
        void SetConfig(ThirdPartyConfig? config);
    }
}
