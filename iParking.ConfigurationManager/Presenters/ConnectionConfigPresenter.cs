using iParking.ConfigurationManager.Services;
using iParking.ConfigurationManager.Views.Interfaces;

namespace iParking.ConfigurationManager.Presenters
{
    public class ConnectionConfigPresenter
    {
        private readonly IConnectionConfigView _view;
        private readonly IConfigService _configService;

        public ConnectionConfigPresenter(IConnectionConfigView view, IConfigService configService)
        {
            _view = view;
            _configService = configService;

            _view.ConfirmClicked += OnConfirm;
            _view.CancelClicked += (s, e) => _view.Close();

            LoadData();
        }

        private void LoadData()
        {
            _view.ServerConfig = _configService.LoadServerConfig();
            _view.LprConfig = _configService.LoadLprConfig();
            _view.AppOption = _configService.LoadAppOption();
            _view.OemConfig = _configService.LoadOemConfig();
            _view.ThirdPartyConfig = _configService.LoadThirdPartyConfig();
            _view.PaymentConfig = _configService.LoadPaymentConfig();
        }

        private void OnConfirm(object? sender, EventArgs e)
        {
            // Lưu cấu hình thông qua service
            _configService.SaveServerConfig(_view.ServerConfig);
            _configService.SaveLprConfig(_view.LprConfig);
            _configService.SaveAppOption(_view.AppOption);
            _configService.SaveOemConfig(_view.OemConfig);
            _configService.SaveThirdPartyConfig(_view.ThirdPartyConfig);
            _configService.SavePaymentConfig(_view.PaymentConfig);

            _view.ShowMessage("Cấu hình đã lưu thành công!", "Thông báo");
        }
    }
}
