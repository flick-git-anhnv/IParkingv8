using iParkingv8.Object.Objects.Events;
using iParkingv8.Object.Objects.Kiosk;
using iParkingv8.Object.Objects.Payments;
using iParkingv8.Ultility.Style;
using IParkingv8.API.Interfaces;
using IParkingv8.Cash.Events;
using IParkingv8.Cash.Factory;
using IParkingv8.Cash.Model;
using Kztek.Object;
using Kztek.Tool;
using Newtonsoft.Json.Linq;

namespace Kztek.Control8.KioskOut.PaymentPresenter.CASH
{
    public class CashPresenter
    {
        private readonly ICashView _view;
        private TaskCompletionSource<BaseKioskResult>? _processCompletionSource;
        private EmKioskUserType _kioskUserType = EmKioskUserType.Customer;

        private ExitData? exitData;
        private readonly PaymentKioskConfig paymentKioskConfig;
        private readonly ICashController cashController;
        private readonly IAPIServer server;

        private DateTime lastCallTime;
        //public bool cho_phep_nuot_tien = false;
        //public bool hoan_tat_thanh_toan = false;
        public readonly SemaphoreSlim semaphoreSlimOnNewEvent = new(1, 1);

        public CashPresenter(ICashView cashView, PaymentKioskConfig config, IAPIServer apiServer)
        {
            this.paymentKioskConfig = config;
            this.server = apiServer;
            _view = cashView;

            this.cashController = CashDeviceFactory.CreateController((EmCashControllerType)config.CashDeviceType, this.paymentKioskConfig);
            _view.OnBackClicked += View_OnBackClicked;
        }
        public async Task<bool> ConnectToDevice()
        {
            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Connect Cash"));
            if (this.cashController is null)
            {
                return false;
            }

            bool isConnectSuccess = await this.cashController.Connect();
            if (isConnectSuccess)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Connect Cash false"));

                this.cashController!.PollEvent += CashPresenter_PollEvent;
                this.cashController!.PollingStart();
            }

            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Connect Cash false"));

            return isConnectSuccess;
        }

        public async Task<BaseKioskResult> ShowView(ExitData exitData)
        {
            this.exitData = exitData;
            _processCompletionSource = new TaskCompletionSource<BaseKioskResult>();
            this.exitData = exitData;
            _kioskUserType = EmKioskUserType.Customer;

            _view.ClearView();
            _view.DisplayEventInfo(exitData);
            _view.ShowView();

            return await _processCompletionSource.Task;
        }
        private async void View_OnBackClicked(object? sender, EventArgs e)
        {
            this._view.ShowLoading(KZUIStyles.CurrentResources.DisconnectingWithDevice, KZUIStyles.CurrentResources.WaitAMoment);

            await StopPayment();

            this._view.HideLoadingIndicator();

            _view.HideView();
            _kioskUserType = EmKioskUserType.Customer;
            _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = false, kioskUserType = _kioskUserType });
        }
        public void Translate()
        {
            _view.SetInfoText(KZUIStyles.CurrentResources.PaymentRequired);
            _view.SetTitleText(KZUIStyles.CurrentResources.PaymentCashTitle);
            _view.SetSubTitleText(KZUIStyles.CurrentResources.PaymentCashSubTitle);

            _view.SetAccessKeyNameTitleText(KZUIStyles.CurrentResources.AccesskeyName);
            _view.SetTimeInTitleText(KZUIStyles.CurrentResources.TimeIn);
            _view.SetPlateInTitleText(KZUIStyles.CurrentResources.PlateIn);
            _view.SetVehicleTypeTitleText(KZUIStyles.CurrentResources.VehicleType);
            _view.SetTimeOutTitleText(KZUIStyles.CurrentResources.TimeOut);
            _view.SetPlateOutTitleText(KZUIStyles.CurrentResources.PlateOut);
            _view.SetFeeTitleText(KZUIStyles.CurrentResources.Fee);
            _view.SetPaidTitleText(KZUIStyles.CurrentResources.Paid);
            _view.SetRemainTitleText(KZUIStyles.CurrentResources.Remain);
            _view.SetBtnBackText(KZUIStyles.CurrentResources.Back);
        }

        /// <summary>
        /// Quy trình xử lý sự kiện </br>
        /// B1: Kiểm tra đã đọc được mệnh giá tiền hay chưa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="cash"></param>
        /// <returns></returns>
        private async Task CashPresenter_PollEvent(object sender, CashResult cash)
        {
            try
            {
                await semaphoreSlimOnNewEvent.WaitAsync();
                //if (hoan_tat_thanh_toan)
                //{
                //    return;
                //}
                if (cash.IsStackerFull)
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "CashPresenter",
                        Description = $"Cảnh báo ngăn chứa tiền đã đầy ",
                    });
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Cảnh báo ngăn chứa tiền đã đầy"));
                }

                //Bắt đầu đưa tiền vào là có Money = 0
                if (cash.MoneyValue == 0)
                {
                    // Các TH không đọc được mệnh giá tiền, đút tiền không thẳng, tiền giả, ....
                    // Reset quy trình
                    if (cash.IsRejected || cash.IsRejecting)
                    {
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CashPresenter",
                            Description = $"Không đọc được mệnh giá tiền - Reject",
                        });
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Không đọc được mệnh giá tiền - Reject"));
                        ClearCash(cash);
                        cash.cho_phep_nuot_tien = false;
                    }
                    //Tiền đang tiếp tục được nuốt vào
                    else
                    {
                        // Chưa có sự kiện đút tiền / hoặc có TH mới có sự kiện đút tiền nhưng không có lệnh Reject
                        // Hoặc đang Disable
                        cash.cho_phep_nuot_tien = false;
                    }
                    return;
                }

                //Sau khi readNode và ko bị reject
                //if (!cash.cho_phep_nuot_tien)
                if (!cash.cho_phep_nuot_tien)
                {
                    // Gọi api thanh toán
                    PaymentEventArgs paymentArgs = new()
                    {
                        PayTime = DateTime.Now,
                        PayValue = (int)cash.MoneyValue,
                        IsSuccess = true,
                        Message = EMPayMessage.Success
                    };
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "CashPresenter",
                        Description = $"Call Api thanh toán: money = {paymentArgs.PayValue}",
                    });

                    // Gửi api thanh toán
                    await SendPaymentApi(paymentArgs, cash);

                    if (!cash.cho_phep_nuot_tien)
                    {
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CashPresenter",
                            Description = $"Gửi api cập nhật tiền thất bại ->  Gửi lệnh Reject tiền: money = {paymentArgs.PayValue}",
                        });
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Gửi api cập nhật tiền thất bại ->  Gửi lệnh Reject tiền"));

                        cashController.RejectMoney();
                        ClearCash(cash);
                    }
                }

                // Nếu đã đọc được mệnh giá tiền
                // Kiểm tra đã gọi api thanh toán thành công chưa 
                else
                {
                    // Kiểm tra tiền đã xuống ngăn chứa cuối chưa,
                    // nếu đã xuống thì tiếp tục chờ người dùng đút tiền nếu chưa đủ
                    // Nếu chưa xuống thì kiểm tra xem là lỗi chưa xác định hay tiền không hợp lệ, và gửi api trừ số tiền đã thanh toán
                    if (cash.IsValidMoney)
                    {
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CashPresenter",
                            Description = $"Tiền đã xuống khay chứa cuối",
                        });
                        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Tiền đã xuống khay chứa cuối"));

                        PaymentEventArgs paymentArgs = new()
                        {
                            PayTime = DateTime.Now,
                            PayValue = (int)cash.MoneyValue,
                            IsSuccess = true,
                            Message = EMPayMessage.Success
                        };

                        UcCash_onTienXuongKhayEvent(paymentArgs);

                        cash.cho_phep_nuot_tien = false;
                        ClearCash(cash);
                    }
                    else
                    {
                        // Nếu chưa xuống ngăn chứa -> Check có phải bị Reject không
                        if (cash.IsRejected)
                        {
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "CashPresenter",
                                Description = $"Đã đọc được mệnh giá tiền nhưng bị Reject",
                            });
                            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Đã đọc được mệnh giá tiền nhưng bị Reject"));

                            // Gửi trạng thái false
                            PaymentEventArgs paymentArgs = new()
                            {
                                PayTime = DateTime.Now,
                                PayValue = (int)cash.MoneyValue,
                                IsSuccess = false,
                                Message = EMPayMessage.Error
                            };
                            UcCash_onTienXuongKhayEvent(paymentArgs);

                            bool cap_nhat_thong_tin_thanh_cong = await SavePayment((int)cash.MoneyValue * -1);

                            ClearCash(cash);
                            cash.cho_phep_nuot_tien = false;
                        }
                        else
                        {
                            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Các trường hợp đặc biệt chưa xác định"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Exception CashPresenter",
                    Description = ex.Message,
                });
            }
            finally
            {
                semaphoreSlimOnNewEvent.Release();
            }
        }
        private async Task SendPaymentApi(PaymentEventArgs e, CashResult cash)
        {
            if (this.exitData?.Amount <= 0)
            {
                cash.cho_phep_nuot_tien = false;
                return;
            }
            //// Gửi thông tin thanh toán tiền mặt
            bool cap_nhat_thong_tin_thanh_cong = await SavePayment(e.PayValue);

            if (cap_nhat_thong_tin_thanh_cong)
            {
                cash.cho_phep_nuot_tien = true;
            }
            else
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Lưu thông tin giao dịch lỗi: {e.PayValue}"));
                cash.cho_phep_nuot_tien = false;
                return;
            }
        }
        private async void UcCash_onTienXuongKhayEvent(PaymentEventArgs paymentEventArgs)
        {
            bool isThan60 = false;

            if ((paymentEventArgs.PayTime - lastCallTime).TotalSeconds > 60)
            {
                isThan60 = true;
            }

            if (paymentEventArgs.IsSuccess)
            {
                _view.DisplayEventInfo(exitData!);

                //Lớn hơn 60s cập nhật lại phí gửi xe còn không thì cập nhật lại thông tin local
                if (isThan60)
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "CashPresenter",
                        Description = $"Than 60s -> update lai tien",
                    });
                    SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Than 60s -> update lai tien"));
                    //await Task.Run(UpdateLargerThan60Second_Kien);
                }

                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "CashPresenter",
                    Description = $"{paymentEventArgs.PayValue} - UpdateView success - TT: currentExitData.Amount = {this.exitData?.Amount}, currentExitData.Entry.Amount = {this.exitData?.Entry?.Amount}, currentExitData.DiscountAmount = {this.exitData?.DiscountAmount}",
                });
                // Lưu log thanh cong
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"{paymentEventArgs.PayValue} - UpdateView success - TT: currentExitData.Amount = {this.exitData?.Amount}, currentExitData.Entry.Amount = {this.exitData?.Entry?.Amount}, currentExitData.DiscountAmount = {this.exitData?.DiscountAmount}"));

                //Kiểm tra nếu đã thanh toán đủ thì thông báo
                if (this.exitData!.Amount - this.exitData.Entry!.Amount - this.exitData.DiscountAmount <= 0)
                {
                    await StopPayment();
                    _view.HideView();
                    _kioskUserType = EmKioskUserType.Customer;
                    _processCompletionSource?.SetResult(new BaseKioskResult { IsConfirm = true, kioskUserType = _kioskUserType });
                    return;
                }
            }
            else
            {
                // Tiền báo đã xuống ngăn cuối nhưng thực chất bị Reject
                // Gửi số tiền âm cập nhật lại thanh toán

                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "CashPresenter",
                    Description = $"Tiền xuống khay nhưng lại Reject - Gửi số tiền âm cập nhật lại thanh toán money = {paymentEventArgs.PayValue * -1}",
                });

                bool cap_nhat_thong_tin_thanh_cong = await SavePayment(paymentEventArgs.PayValue * -1);
            }
        }

        private static void ClearCash(CashResult cash)
        {
            cash.MoneyValue = 0;
            cash.IsValidMoney = false;
            cash.IsRejected = false;
            cash.IsRejecting = false;
            cash.IsCallAPIPaymentSuccess = false;
            cash.IsNoteStacking = false;
            cash.IsWarning = false;
            cash.Describe = "";
            cash.Plate = "";
            cash.IsStackerFull = false;
            cash.cho_phep_nuot_tien = false;
        }

        public async Task StopPayment()
        {
            try
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Stop cash payment"));
                //this.cho_phep_nuot_tien = false;
                //this.hoan_tat_thanh_toan = false;
                cashController.PollEvent -= CashPresenter_PollEvent;
                await cashController.PollingStop();
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Stop cash payment error", ex));
            }
        }
        //public async void UpdateLargerThan60Second_Kien()
        //{
        //    await Task.Run(new Action(async () =>
        //    {
        //        //Call lại cập nhật số tiền
        //        //Có thay đổi thì cập nhật lên view

        //        SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"Call api cập nhật lại tiền"));
        //        var response = await this.server.PaymentService.GetVehicleById(exitData?.Id, true);
        //        if (response == null)
        //        {
        //            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"lấy thông tin null"));
        //            return;
        //        }
        //        else
        //        {
        //            SystemUtils.logger.SaveSystemLog(SystemLog.CreateApplicationProccess($"CurrenExitData = {Newtonsoft.Json.JsonConvert.SerializeObject(response)}"));

        //            //Lấy thông tin thành công ==> Cập nhật view
        //            this.exitData = response;

        //            // Cập nhật view
        //            _view.DisplayEventInfo(exitData);

        //            // Nếu lấy thông tin thành công set lại mốc Time (1 phút)
        //            lastCallTime = DateTime.Now;
        //        }
        //    }));
        //}

        //Sự kiện ra, nhưng cần tạo thông tin thanh toan cho sự kiện vào tránh trường hợp chưa thanh toán xong nhưng bấm back
        //==> mất sự kiện
        private async Task<bool> SavePayment(int paid)
        {
            string description = "";
            PaymentRequest paymentRequest = new(this.exitData!.Entry!.Id, OrderMethod.CASH, paid, description, targetType: TargetType.ENTRY);
            var cashDetail = await this.server.PaymentService.CreateTransactionAsync(paymentRequest);

            if (cashDetail is null)
            {
                return false;
            }

            this.exitData.Entry.Amount += paid;
            return true;
        }
    }
}
