using iParkingv8.Object.ConfigObjects.AppConfigs;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface IAppOptionView
    {
        AppOption? GetConfig();
        void SetConfig(AppOption? config);

        void DisplayDevelopMode(bool displayDevelopMode);
    }
}
