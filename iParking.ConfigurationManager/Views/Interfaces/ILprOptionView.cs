using Kztek.Object;

namespace iParking.ConfigurationManager.Views.Interfaces
{
    public interface ILprOptionView
    {
        LprConfig? GetConfig();
        void SetConfig(LprConfig? config);
    }
}
