using iParkingv8.Object.Objects.Payments;
using IParkingv8.Cash.Events;
using IParkingv8.Cash.Factory;
using IParkingv8.Cash.Helpers;
using IParkingv8.Cash.Model;
using IPGS.Cash;
using Kztek.Tool;
using System.ComponentModel.DataAnnotations;
using System.IO.Ports;
using System.Text;

namespace IParkingv8.Cash.Controllers
{
    public class CBA9ControllersV2 : ICashController
    {
        // ssp library variables
        SSPComms m_eSSP;
        SSP_COMMAND m_cmd;
        SSP_KEYS keys;
        SSP_FULL_KEY sspKey;
        SSP_COMMAND_INFO info;
        Thread ConnectionThread;
        // variable declarations

        // The comms window class, used to log everything sent to the validator visually and to file
        CCommsWindow m_Comms;

        // The protocol version this validator is using, set in setup request
        int m_ProtocolVersion;

        // A variable to hold the type of validator, this variable is initialised using the setup request command
        char m_UnitType;

        // Two variables to hold the number of notes accepted by the validator and the value of those
        // notes when added up
        int m_NumStackedNotes;

        // Variable to hold the number of channels in the validator dataset
        int m_NumberOfChannels;

        // The multiplier by which the channel values are multiplied to give their
        // real penny value. E.g. £5.00 on channel 1, the value would be 5 and the multiplier
        // 100.
        int m_ValueMultiplier;

        //Integer to hold total number of Hold messages to be issued before releasing note from escrow
        int m_HoldNumber;

        //Integer to hold number of hold messages still to be issued
        int m_HoldCount;

        //Bool to hold flag set to true if a note is being held in escrow
        bool m_NoteHeld;

        // A list of dataset data, sorted by value. Holds the info on channel number, value, currency,
        // level and whether it is being recycled.
        List<ChannelData> m_UnitDataList;

        // Event 
        public event ConnectCBA9EventHandle ConnectionStatusEvent;
        public event PollCBA9EventHandle PollEvent;
        // Class
        //public CancellationTokenSource cts;

        // Constant
        int reconnectionAttempts = 10, reconnectionInterval = 3;
        System.Windows.Forms.Timer reconnectionTimer = new System.Windows.Forms.Timer();


        private readonly PaymentKioskConfig paymentKioskConfig;
        public CBA9ControllersV2(PaymentKioskConfig paymentKioskConfig)
        {
            this.paymentKioskConfig = paymentKioskConfig;
            m_eSSP = new SSPComms();
            m_cmd = new SSP_COMMAND();
            keys = new SSP_KEYS();
            sspKey = new SSP_FULL_KEY();
            info = new SSP_COMMAND_INFO();

            m_Comms = new CCommsWindow("NoteValidator");
            m_NumberOfChannels = 0;
            m_ValueMultiplier = 1;
            m_UnitType = (char)0xFF;
            m_UnitDataList = new List<ChannelData>();
            m_HoldCount = 0;
            m_HoldNumber = 0;
        }
        public SSPComms SSPComms
        {
            get { return m_eSSP; }
            set { m_eSSP = value; }
        }

        // a pointer to the command structure, this struct is filled with info and then compiled into
        // a packet by the library and sent to the validator
        public SSP_COMMAND CommandStructure
        {
            get { return m_cmd; }
            set { m_cmd = value; }
        }

        // pointer to an information structure which accompanies the command structure
        public SSP_COMMAND_INFO InfoStructure
        {
            get { return info; }
            set { info = value; }
        }

        // access to the comms log for recording new log messages
        public CCommsWindow CommsLog
        {
            get { return m_Comms; }
            set { m_Comms = value; }
        }

        // access to the type of unit, this will only be valid after the setup request
        public char UnitType
        {
            get { return m_UnitType; }
        }

        // access to number of channels being used by the validator
        public int NumberOfChannels
        {
            get { return m_NumberOfChannels; }
            set { m_NumberOfChannels = value; }
        }

        // access to number of notes stacked
        public int NumberOfNotesStacked
        {
            get { return m_NumStackedNotes; }
            set { m_NumStackedNotes = value; }
        }
        // access to value multiplier
        public int Multiplier
        {
            get { return m_ValueMultiplier; }
            set { m_ValueMultiplier = value; }
        }
        // acccess to hold number
        public int HoldNumber
        {
            get { return m_HoldNumber; }
            set { m_HoldNumber = value; }

        }
        //Access to flag showing note is held in escrow
        public bool NoteHeld
        {
            get { return m_NoteHeld; }
        }
        public bool IsStopGetEvent { get; set; } = false;
        public CancellationTokenSource cts { get; set; }
        public async Task<bool> Connect()
        {
            GetComPort();
            if (await ConnectToValidator())
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "ConnectToValidator",
                    Description = "Connect = true",
                });

                isConnected = true;
            }
            else
            {
                isConnected = false;
            }
            return isConnected;
        }
        private Task _pollingTask;
        public async void PollingStart()
        {
            try
            {
                if (cts == null || cts.IsCancellationRequested)
                {
                    IsStopGetEvent = false;
                    cts = new CancellationTokenSource();
                    _pollingTask = PollingGetEventFunc(cts.Token);
                }
                else
                {
                    IsStopGetEvent = false;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Polling Start",
                    Description = ex.Message,
                });
            }
        }

        public async Task PollingStop()
        {
            try
            {
                IsStopGetEvent = true;
                cts?.Cancel();

                if (_pollingTask != null)
                {
                    try
                    {
                        await _pollingTask; // Đợi task kết thúc
                    }
                    catch (OperationCanceledException) { } // bỏ qua cancel lỗi
                }

                await Task.Run(new Action(() =>
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (DisableValidator())
                        {
                            break;
                        }
                    }
                    //if (DisableValidator())
                    //{

                    //}

                    SSPComms.CloseComPort(); // Đóng COM cuối cùng
                }));
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Polling Stop",
                    Description = ex.Message,
                });
            }
        }

        public void GetComPort()
        {
            try
            {
                CommandStructure.ComPort = paymentKioskConfig.CashComport;
                CommandStructure.SSPAddress = 0;
                CommandStructure.Timeout = 10000;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "GetComPort Exception",
                    Description = ex.Message,
                });
            }
        }
        public async Task PollingGetEventFunc(CancellationToken token)
        {
            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceId = "CASH",
                Cmd = "PollingGetEventFunc",
                Description = "Bắt đầu DoPoll",
            });

            CashResult cash = new();

            //đề phòng t/h chưa có kết nối
            if (!isConnected)
            {
                PollEvent?.Invoke(this, cash);
                return;
            }

            //Bắt đầu polling
            while (!token.IsCancellationRequested)
            {
                if (!IsStopGetEvent)
                {
                    try
                    {
                        if (!await DoPoll(cash))
                        {
                            await ConnectToValidator();
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                await Task.Delay(100, token);
            }
        }
        bool isConnected = false;
        private async Task<bool> ConnectToValidator()
        {
            return await Task.Run(() =>
              {
                  try
                  {
                      SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                      {
                          DeviceId = "CASH",
                          Cmd = "ConnectToValidator",
                          Description = "Bắt đầu ConnectToValidator",
                      });

                      if (!isConnected)
                      {
                          // setup the timer
                          reconnectionTimer.Interval = reconnectionInterval * 1000; // for ms

                          // run for number of attempts specified
                          // reset timer
                          reconnectionTimer.Enabled = true;

                          // close com port in case it was open
                          SSPComms.CloseComPort();

                          // turn encryption off for first stage
                          CommandStructure.EncryptionStatus = false;

                          //if (isDisable) return false;

                          // Nếu đã có kết nối
                          if (m_eSSP.comPort != null)
                          {
                              if (m_eSSP.comPort.IsOpen)
                              {
                                  return true;
                              }
                          }

                          if (OpenComPort() && NegotiateKeys())
                          {
                              CommandStructure.EncryptionStatus = true;
                              // now encrypting
                              //find the max protocol version this validator supports
                              byte maxPVersion = FindMaxProtocolVersion();
                              if (maxPVersion > 6)
                              {
                                  SetProtocolVersion(maxPVersion);
                              }
                              else
                              {

                                  SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                                  {
                                      DeviceId = "CASH",
                                      Cmd = "ConnectToValidator",
                                      Description = "This program does not support units under protocol version 6, update firmware.",
                                  });
                                  return false;
                              }
                              // get info from the validator and store useful vars
                              ValidatorSetupRequest();
                              // Get Serial number
                              GetSerialNumber();
                              // check this unit is supported by this program
                              if (!IsUnitTypeSupported(UnitType))
                              {
                                  SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                                  {
                                      DeviceId = "CASH",
                                      Cmd = "ConnectToValidator",
                                      Description = "Unsupported unit type, this SDK supports the BV series and the NV series (excluding the NV11)",
                                  });
                                  Reset();
                                  //Application.Exit();
                                  return false;
                              }
                              // inhibits, this sets which channels can receive notes
                              SetInhibits();
                              // enable, this allows the validator to receive and act on commands
                              EnableValidator();
                              isConnected = true;
                              return true;
                          }
                      }
                      else
                      {
                          //if (isDisable) return false;
                          EnableValidator();
                          isConnected = true;
                          return true;
                      }

                      return false;
                  }
                  catch (Exception ex)
                  {
                      isConnected = false;
                      SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                      {
                          DeviceId = "CASH",
                          Cmd = "ConnectToValidator",
                          Description = ex.Message,
                      });
                      return false;
                  }

              });

        }
        public async Task<bool> DoPoll(CashResult cash)
        {
            try
            {
                byte i;
                // If a not is to be held in escrow, send hold commands, as poll releases note.
                if (m_HoldCount > 0)
                {
                    m_NoteHeld = true;
                    m_HoldCount--;
                    m_cmd.CommandData[0] = CCommands.SSP_CMD_HOLD;
                    m_cmd.CommandDataLength = 1;
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "DoPoll",
                        Description = "Note held in escrow: " + m_HoldCount,
                    });
                    if (!SendCommand()) return false;
                    return true;

                }

                //send poll
                m_cmd.CommandData[0] = CCommands.SSP_CMD_POLL;
                m_cmd.CommandDataLength = 1;
                m_NoteHeld = false;

                if (!SendCommand())
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "DoPoll",
                        Description = "Start DoPol False",
                    });
                    return false;
                }

                //Nếu không có sự kiện thì ResponseDataLength = 0 || 1
                //Chỉ để log lại dữ liệu
                if (m_cmd.ResponseDataLength != 1)
                {
                    string[] values = new string[m_cmd.ResponseDataLength];
                    for (int a = 0; a < m_cmd.ResponseDataLength; a++)
                    {
                        values[a] = m_cmd.ResponseData[a].ToString("X2");
                    }
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "DoPoll",
                        Description = $"Gửi lệnh POLL Nhận m_cmd.ResponseData = {Newtonsoft.Json.JsonConvert.SerializeObject(values)} ",
                    });
                }

                //SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                //{
                //    DeviceId = "CASH",
                //    Cmd = "DoPoll",
                //    Description = "Start DoPol",
                //});
                //parse poll response
                int noteVal = 0;

                //B1: Có người đút tiền vào ==> trả về sự kiện SSP_POLL_READ_NOTE, Sẽ bắn nhiều sự kiện readnote 
                for (i = 1; i < m_cmd.ResponseDataLength; i++)
                {
                    // Các sự kiện phản hồi
                    switch (m_cmd.ResponseData[i])
                    {
                        // Phản hồi cho biết đã reset lại thiết bị
                        //case CCommands.SSP_POLL_SLAVE_RESET:
                        //    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        //    {
                        //        DeviceId = "CASH",
                        //        Cmd = "DoPoll",
                        //        Description = "Nhận case: SSP_POLL_SLAVE_RESET - Unit reset",
                        //    });
                        //    break;

                        // Đang đọc cảm biến của trình xác thực
                        // Byte thứ 2 = 0 cho đến khi đọc được mệnh giá tiền
                        case CCommands.SSP_POLL_READ_NOTE:
                            {
                                //READING = Tiền đang đi vào vị trí để đọc
                                if (m_cmd.ResponseData[i + 1] == 0)
                                {
                                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                                    {
                                        DeviceId = "CASH",
                                        Cmd = "DoPoll",
                                        Description = $"Nhận case: SSP_POLL_READ_NOTE - Reading note...",
                                    });
                                    m_HoldCount = m_HoldNumber;
                                    cash.MoneyValue = long.Parse(CHelpers.FormatToCurrency(noteVal));
                                }
                                //Đã tới vị trí đọc ==> bị reject | được tiếp tục
                                else
                                {
                                    noteVal = GetChannelValue(m_cmd.ResponseData[i + 1]);//Số tiền
                                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                                    {
                                        DeviceId = "CASH",
                                        Cmd = "DoPoll",
                                        Description = $"Nhận case: SSP_POLL_READ_NOTE - Note in escrow, amount: {CHelpers.FormatToCurrency(noteVal)} + {GetChannelCurrency(m_cmd.ResponseData[i + 1])}",
                                    });
                                    m_HoldCount = m_HoldNumber;
                                    cash.MoneyValue = long.Parse(CHelpers.FormatToCurrency(noteVal));
                                }
                                i++;
                            }
                            break;

                        #region Bị Reject
                        // Đang từ chối tiền bởi trình xác thực
                        case CCommands.SSP_POLL_NOTE_REJECTING:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_NOTE_REJECTING - Rejecting note...",
                            });
                            cash.IsRejecting = true;
                            break;
                        // Đã bị từ chỗi bởi trình xác thực
                        case CCommands.SSP_POLL_NOTE_REJECTED:

                            cash.IsRejected = true;
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_NOTE_REJECTED - Note rejected",
                            });
                            SaveRejectReasonLog();
                            break;
                        #endregion

                        // Tiền đang chuyển tới khay cuối, vẫn có thể bị lỗi và nhả ra tiền
                        case CCommands.SSP_POLL_NOTE_STACKING:
                            {
                                cash.IsNoteStacking = true;
                                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                                {
                                    DeviceId = "CASH",
                                    Cmd = "DoPoll",
                                    Description = $"Nhận case: SSP_POLL_NOTE_STACKING - Stacking note...",
                                });

                            }
                            break;

                        // Xác nhận tờ tiền hợp lệ
                        case CCommands.SSP_POLL_CREDIT_NOTE:
                            noteVal = GetChannelValue(m_cmd.ResponseData[i + 1]);
                            string textMoney = CHelpers.FormatToCurrency(noteVal) + " " + GetChannelCurrency(m_cmd.ResponseData[i + 1]);
                            cash.IsValidMoney = true;
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_CREDIT_NOTE - Credit: {textMoney}",
                            });
                            //cash.TurnMoney++;
                            i++;
                            break;

                        // Tiền đã chuyển tới vị trí cuối - kết thúc chu trình
                        case CCommands.SSP_POLL_NOTE_STACKED:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_NOTE_STACKED - Note stacked",
                            });
                            break;
                        // Ngăn chứa tiền đã đầy
                        case CCommands.SSP_POLL_STACKER_FULL:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_STACKER_FULL - Stacker full",
                            });
                            cash.IsStackerFull = true;
                            break;

                        #region LogOnly
                        // Kẹt tiền an toàn
                        case CCommands.SSP_POLL_SAFE_NOTE_JAM:

                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_SAFE_NOTE_JAM - Safe jam",
                            });
                            break;
                        // Kẹt tiền không an toàn
                        case CCommands.SSP_POLL_UNSAFE_NOTE_JAM:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_UNSAFE_NOTE_JAM - Unsafe jam",
                            });
                            break;
                        // Ngừng cho phép CBA9 hoạt động 
                        case CCommands.SSP_POLL_DISABLED:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_DISABLED - Disable",
                            });

                            break;
                        // Cảnh báo có gian lận
                        case CCommands.SSP_POLL_FRAUD_ATTEMPT:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_FRAUD_ATTEMPT - Fraud attempt, note type: + {GetChannelValue(m_cmd.ResponseData[i + 1])}",
                            });
                            i++;
                            break;

                        // Một ghi chú được phát hiện ở đâu đó bên trong trình xác thực khi khởi động và bị từ chối ở phía trước đơn vị.
                        case CCommands.SSP_POLL_NOTE_CLEARED_FROM_FRONT:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"SSP_POLL_NOTE_CLEARED_FROM_FRONT - {GetChannelValue(m_cmd.ResponseData[i + 1])} +note cleared from front at reset",
                            });
                            i++;
                            break;
                        // Một ghi chú được phát hiện ở đâu đó bên trong trình xác thực khi khởi động và được đưa xuống cashbox.
                        case CCommands.SSP_POLL_NOTE_CLEARED_TO_CASHBOX:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_NOTE_CLEARED_TO_CASHBOX - {GetChannelValue(m_cmd.ResponseData[i + 1])} + note cleared to stacker at reset",
                            });
                            i++;
                            break;
                        // Hộp tiền mặt đã được tháo ra khỏi thiết bị 
                        case CCommands.SSP_POLL_CASHBOX_REMOVED:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_CASHBOX_REMOVED - Cashbox removed..."
                            });
                            break;
                        // Hộp tiền đã được thay thế 
                        case CCommands.SSP_POLL_CASHBOX_REPLACED:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_CASHBOX_REPLACED - Cashbox replaced",
                            });
                            break;
                        // A bar code ticket has been detected and validated. The ticket is in escrow, continuing to poll will accept
                        // the ticket, sending a reject command will reject the ticket.
                        //case CCommands.SSP_POLL_BAR_CODE_VALIDATED:
                        //    log.AppendText("Bar code ticket validated\r\n");
                        //    break;
                        //// A bar code ticket has been accepted (equivalent to note credit).
                        //case CCommands.SSP_POLL_BAR_CODE_ACK:
                        //    log.AppendText("Bar code ticket accepted\r\n");
                        //    break;
                        // Nắp tờ tiền đang mở, hệ thống tự ngắt nguồn
                        case CCommands.SSP_POLL_NOTE_PATH_OPEN:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_NOTE_PATH_OPEN - Note path open",
                            });
                            break;
                        // All channels on the validator have been inhibited so the validator is disabled. Only available on protocol
                        // versions 7 and above.
                        case CCommands.SSP_POLL_CHANNEL_DISABLE:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: SSP_POLL_CHANNEL_DISABLE - All channels inhibited, unit disabled",
                            });

                            break;
                        default:
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "DoPoll",
                                Description = $"Nhận case: khác loại - Unrecognised poll response detected  {(int)m_cmd.ResponseData[i]} ",
                            });
                            break;
                            #endregion

                    }
                }

                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "DoPoll",
                    Description = "Check Count" + Newtonsoft.Json.JsonConvert.SerializeObject(cash),
                });
                PollEvent?.Invoke(this, cash);
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "DoPoll",
                    Description = ex.Message,
                });
                return false;
            }

        }
        public bool OpenComPort()
        {
            try
            {
                if (!m_eSSP.OpenSSPComPort(m_cmd))
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "OpenComPort",
                        Description = $"Mở ConPort thất bại",
                    });
                    return false;
                }
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "OpenComPort",
                    Description = $"Mở ConPort thành công",
                });
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "OpenComPort",
                    Description = $"Mở ConPort thất bại",
                    Response = ex.Message,
                });
                return false;
            }

        }
        public bool NegotiateKeys()
        {
            try
            {
                // make sure encryption is off
                m_cmd.EncryptionStatus = false;

                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Syncing",
                });
                m_cmd.CommandData[0] = CCommands.SSP_CMD_SYNC;
                m_cmd.CommandDataLength = 1;

                if (!SendCommand()) return false;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Success",
                });
                m_eSSP.InitiateSSPHostKeys(keys, m_cmd);

                // send generator
                m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_GENERATOR;
                m_cmd.CommandDataLength = 9;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Setting generator...",
                });
                // Convert generator to bytes and add to command data.
                BitConverter.GetBytes(keys.Generator).CopyTo(m_cmd.CommandData, 1);

                if (!SendCommand()) return false;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Success",
                });

                // send modulus
                m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_MODULUS;
                m_cmd.CommandDataLength = 9;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Sending modulus... ",
                });
                // Convert modulus to bytes and add to command data.
                BitConverter.GetBytes(keys.Modulus).CopyTo(m_cmd.CommandData, 1);

                if (!SendCommand()) return false;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Success",
                });
                // send key exchange
                m_cmd.CommandData[0] = CCommands.SSP_CMD_REQUEST_KEY_EXCHANGE;
                m_cmd.CommandDataLength = 9;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Exchanging keys",
                });
                // Convert host intermediate key to bytes and add to command data.
                BitConverter.GetBytes(keys.HostInter).CopyTo(m_cmd.CommandData, 1);


                if (!SendCommand()) return false;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = $"Success",
                });
                // Read slave intermediate key.
                keys.SlaveInterKey = BitConverter.ToUInt64(m_cmd.ResponseData, 1);

                m_eSSP.CreateSSPHostEncryptionKey(keys);

                // get full encryption key
                m_cmd.Key.FixedKey = 0x0123456701234567;
                m_cmd.Key.VariableKey = keys.KeyHost;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = "Keys successfully negotiated",
                });
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "NegotiateKeys",
                    Description = ex.Message,
                });
                return false;
            }

        }
        public void SetProtocolVersion(byte pVersion)
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_HOST_PROTOCOL_VERSION;
                m_cmd.CommandData[1] = pVersion;
                m_cmd.CommandDataLength = 2;
                if (!SendCommand()) return;
            }
            catch (Exception ex) 
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "SetProtocolVersion - Exception",
                    Description = ex.Message,
                });

            }
            finally
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "SetProtocolVersion",
                    Description = "Finish",
                });
            }
           
        }
        private byte FindMaxProtocolVersion()
        {
            try
            {
                // not dealing with protocol under level 6
                // attempt to set in validator
                byte b = 0x06;
                while (true)
                {
                    SetProtocolVersion(b);
                    if (CommandStructure.ResponseData[0] == CCommands.SSP_RESPONSE_FAIL)
                        return --b;
                    b++;
                    if (b > 20)
                        return 0x06; // return default if protocol 'runs away'
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "FindMaxProtocolVersion",
                    Description = ex.Message,
                });
                return 0x06;
            }
            finally
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "FindMaxProtocolVersion",
                    Description = "Finish",
                });
            }


        }
        public void ValidatorSetupRequest()
        {
            StringBuilder sbDisplay = new StringBuilder(1000);

            // send setup request
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SETUP_REQUEST;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand()) return;

            // display setup request


            // unit type
            int index = 1;
            sbDisplay.Append("Unit Type: ");
            m_UnitType = (char)m_cmd.ResponseData[index++];
            switch (m_UnitType)
            {
                case (char)0x00: sbDisplay.Append("Validator"); break;
                case (char)0x03: sbDisplay.Append("SMART Hopper"); break;
                case (char)0x06: sbDisplay.Append("SMART Payout"); break;
                case (char)0x07: sbDisplay.Append("NV11"); break;
                case (char)0x0D: sbDisplay.Append("TEBS"); break;
                default: sbDisplay.Append("Unknown Type"); break;
            }

            // firmware
            sbDisplay.AppendLine();
            sbDisplay.Append("Firmware: ");

            sbDisplay.Append((char)m_cmd.ResponseData[index++]);
            sbDisplay.Append((char)m_cmd.ResponseData[index++]);
            sbDisplay.Append(".");
            sbDisplay.Append((char)m_cmd.ResponseData[index++]);
            sbDisplay.Append((char)m_cmd.ResponseData[index++]);

            sbDisplay.AppendLine();
            // country code.
            // legacy code so skip it.
            index += 3;

            // value multiplier.
            // legacy code so skip it.
            index += 3;

            // Number of channels
            sbDisplay.AppendLine();
            sbDisplay.Append("Number of Channels: ");
            m_NumberOfChannels = m_cmd.ResponseData[index++];
            sbDisplay.Append(m_NumberOfChannels);
            sbDisplay.AppendLine();

            // channel values.
            // legacy code so skip it.
            index += m_NumberOfChannels; // Skip channel values

            // channel security
            // legacy code so skip it.
            index += m_NumberOfChannels;

            // real value multiplier
            // (big endian)
            sbDisplay.Append("Real Value Multiplier: ");
            m_ValueMultiplier = m_cmd.ResponseData[index + 2];
            m_ValueMultiplier += m_cmd.ResponseData[index + 1] << 8;
            m_ValueMultiplier += m_cmd.ResponseData[index] << 16;
            sbDisplay.Append(m_ValueMultiplier);
            sbDisplay.AppendLine();
            index += 3;


            // protocol version
            sbDisplay.Append("Protocol Version: ");
            m_ProtocolVersion = m_cmd.ResponseData[index++];
            sbDisplay.Append(m_ProtocolVersion);
            sbDisplay.AppendLine();

            // Add channel data to list then display.
            // Clear list.
            m_UnitDataList.Clear();
            // Loop through all channels.

            for (byte i = 0; i < m_NumberOfChannels; i++)
            {
                ChannelData loopChannelData = new ChannelData();
                // Channel number.
                loopChannelData.Channel = (byte)(i + 1);

                // Channel value.
                loopChannelData.Value = BitConverter.ToInt32(m_cmd.ResponseData, index + m_NumberOfChannels * 3 + i * 4) * m_ValueMultiplier;

                // Channel Currency
                loopChannelData.Currency[0] = (char)m_cmd.ResponseData[index + i * 3];
                loopChannelData.Currency[1] = (char)m_cmd.ResponseData[index + 1 + i * 3];
                loopChannelData.Currency[2] = (char)m_cmd.ResponseData[index + 2 + i * 3];

                // Channel level.
                loopChannelData.Level = 0;

                // Channel recycling
                loopChannelData.Recycling = false;

                // Add data to list.
                m_UnitDataList.Add(loopChannelData);

                //Display data
                sbDisplay.Append("Channel ");
                sbDisplay.Append(loopChannelData.Channel);
                sbDisplay.Append(": ");
                sbDisplay.Append(loopChannelData.Value / m_ValueMultiplier);
                sbDisplay.Append(" ");
                sbDisplay.Append(loopChannelData.Currency);
                sbDisplay.AppendLine();
            }

            // Sort the list by .Value.
            m_UnitDataList.Sort((d1, d2) => d1.Value.CompareTo(d2.Value));

            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
            {
                DeviceId = "CASH",
                Cmd = "ValidatorSetupRequest",
                Description = sbDisplay.ToString(),
            });
        }
        private bool IsUnitTypeSupported(char type)
        {
            if (type == (char)0x00)
                return true;
            return false;
        }
        public string GetChannelCurrency(int channelNum)
        {
            if (channelNum >= 1 && channelNum <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channelNum)
                        return new string(d.Currency);
                }
            }
            return "";
        }
        public int GetChannelValue(int channelNum)
        {
            if (channelNum >= 1 && channelNum <= m_NumberOfChannels)
            {
                foreach (ChannelData d in m_UnitDataList)
                {
                    if (d.Channel == channelNum)
                        return d.Value;
                }
            }
            return -1;
        }
        public void SetInhibits()
        {
            //TextBox log = new TextBox();
            // set inhibits
            m_cmd.CommandData[0] = CCommands.SSP_CMD_SET_CHANNEL_INHIBITS;
            m_cmd.CommandData[1] = 0xFF;
            m_cmd.CommandData[2] = 0xFF;
            m_cmd.CommandDataLength = 3;

            if (!SendCommand()) return;
            if (CheckGenericResponses()) //&& log != null)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Inhibits Set",
                    Response = "Success"
                });
            }
            else
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Inhibits Set",
                    Response = "False"
                });
            }
        }
        public void GetSerialNumber(byte Device, TextBox log = null)
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_SERIAL_NUMBER;
            m_cmd.CommandData[1] = Device;
            m_cmd.CommandDataLength = 2;


            if (!SendCommand()) return;
            if (CheckGenericResponses() && log != null)
            {
                // Response data is big endian, so reverse bytes 1 to 4.
                Array.Reverse(m_cmd.ResponseData, 1, 4);
                //log.AppendText("Serial Number Device " + Device + ": ");
                //log.AppendText(BitConverter.ToUInt32(m_cmd.ResponseData, 1).ToString());
                //log.AppendText("\r\n");
            }
        }

        public void GetSerialNumber()
        {
            //TextBox log = new TextBox();
            m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_SERIAL_NUMBER;
            m_cmd.CommandDataLength = 1;

            if (!SendCommand()) return;
            if (CheckGenericResponses())
            {
                // Response data is big endian, so reverse bytes 1 to 4.
                Array.Reverse(m_cmd.ResponseData, 1, 4);
                //log.AppendText("Serial Number ");
                //log.AppendText(": ");
                //log.AppendText(BitConverter.ToUInt32(m_cmd.ResponseData, 1).ToString());
                //log.AppendText("\r\n");
            }
        }
        public bool RejectMoney()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_REJECT_BANKNOTE;
                m_cmd.CommandDataLength = 1;

                if (SendCommand())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "RejectMoney",
                    Response = ex.Message
                });
                return false;
            }

        }
        public void EnableValidator()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_ENABLE;
                m_cmd.CommandDataLength = 1;

                if (!SendCommand()) return;
                if (CheckGenericResponses())
                {
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "EnableValidator",
                    Response = ex.Message
                });
                return;
            }

        }
        public bool EnableValidator22()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_ENABLE;
                m_cmd.CommandDataLength = 1;

                if (!SendCommand()) return false;
                if (CheckGenericResponses())
                {
                }
                else
                {

                }
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "EnableValidator",
                    Response = ex.Message
                });
                return false;
            }

        }
        // Disable command stops the validator from acting on commands.
        public bool DisableValidator()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_DISABLE;
                m_cmd.CommandDataLength = 1;

                if (!SendCommand()) return false;
                // check response
                if (CheckGenericResponses())
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "DisableValidator",
                        Response = "Success"
                    });
                }
                else
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "DisableValidator",
                        Response = "False"
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "DisableValidator",
                    Response = ex.Message
                });
                return false;
            }

        }
        private bool CheckGenericResponses()
        {
            if (m_cmd.ResponseData[0] == CCommands.SSP_RESPONSE_OK)
                return true;
            else
            {
                switch (m_cmd.ResponseData[0])
                {
                    case CCommands.SSP_RESPONSE_COMMAND_CANNOT_BE_PROCESSED:
                        if (m_cmd.ResponseData[1] == 0x03)
                        {
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "CheckGenericResponses",
                                Response = "Validator has responded with \"Busy\", command cannot be processed at this time"
                            });
                        }
                        else
                        {
                            SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                            {
                                DeviceId = "CASH",
                                Cmd = "CheckGenericResponses",
                                Response = "Command response is CANNOT PROCESS COMMAND, error code - 0x" + BitConverter.ToString(m_cmd.ResponseData, 1, 1)
                            });
                        }
                        return false;
                    case CCommands.SSP_RESPONSE_FAIL:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is FAIL"
                        });
                        return false;
                    case CCommands.SSP_RESPONSE_KEY_NOT_SET:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is KEY NOT SET, Validator requires encryption on this command or there is a problem with the encryption on this request\r\n",
                        });
                        return false;
                    case CCommands.SSP_RESPONSE_PARAMETER_OUT_OF_RANGE:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is PARAM OUT OF RANGE"
                        });
                        return false;
                    case CCommands.SSP_RESPONSE_SOFTWARE_ERROR:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is SOFTWARE ERROR"
                        });
                        return false;
                    case CCommands.SSP_RESPONSE_COMMAND_NOT_KNOWN:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is UNKNOWN"
                        });
                        return false;
                    case CCommands.SSP_RESPONSE_WRONG_NO_PARAMETERS:
                        SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                        {
                            DeviceId = "CASH",
                            Cmd = "CheckGenericResponses",
                            Response = "Command response is WRONG PARAMETERS"
                        });
                        return false;
                    default:
                        return false;
                }
            }
        }
        public bool SendCommand()
        {
            byte[] backup = new byte[255];
            m_cmd.CommandData.CopyTo(backup, 0);

            if (m_eSSP.SSPSendCommand(m_cmd, info) == false)
            {
                //m_eSSP.CloseComPort();
                isConnected = false;
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "SendCommand",
                    Response = "CBA9 SendCommand fail: " + m_cmd.ResponseStatus
                });
                return false;
            }
            return true;
        }

        public void Reset()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_RESET;
                m_cmd.CommandDataLength = 1;
                if (!SendCommand()) return;

                if (CheckGenericResponses())
                {
                    SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                    {
                        DeviceId = "CASH",
                        Cmd = "Reset",
                        Response = "Resetting unit\r\n"
                    });
                }
            }
            catch (Exception ex)
            {
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "Reset",
                    Response = ex.Message,
                });
                return;
            }

        }
        public bool GetCounter()
        {
            try
            {
                m_cmd.CommandData[0] = CCommands.SSP_CMD_GET_COUNTERS;
                m_cmd.CommandDataLength = 1;
                if (!SendCommand()) return false;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }






        public void SaveRejectReasonLog()
        {
            m_cmd.CommandData[0] = CCommands.SSP_CMD_LAST_REJECT_CODE;
            m_cmd.CommandDataLength = 1;
            if (!SendCommand()) return;

            if (CheckGenericResponses())
            {
                string message = "";
                switch (m_cmd.ResponseData[1])
                {
                    case 0x00: message = "Note accepted"; break;
                    case 0x01: message = "Note length incorrect\r\n"; break;
                    case 0x02: message = "Invalid note\r\n"; break;
                    case 0x03: message = "Invalid note\r\n"; break;
                    case 0x04: message = "Invalid note\r\n"; break;
                    case 0x05: message = "Invalid note\r\n"; break;
                    case 0x06: message = "Channel inhibited\r\n"; break;
                    case 0x07: message = "Second note inserted during read\r\n"; break;
                    case 0x08: message = "Host rejected note\r\n"; break;
                    case 0x09: message = "Invalid note\r\n"; break;
                    case 0x0A: message = "Invalid note read\r\n"; break;
                    case 0x0B: message = "Note too long\r\n"; break;
                    case 0x0C: message = "Validator disabled\r\n"; break;
                    case 0x0D: message = "Mechanism slow/stalled\r\n"; break;
                    case 0x0E: message = "Strim attempt\r\n"; break;
                    case 0x0F: message = "Fraud channel reject\r\n"; break;
                    case 0x10: message = "No notes inserted\r\n"; break;
                    case 0x11: message = "Invalid note read\r\n"; break;
                    case 0x12: message = "Twisted note detected\r\n"; break;
                    case 0x13: message = "Escrow time-out\r\n"; break;
                    case 0x14: message = "Bar code scan fail\r\n"; break;
                    case 0x15: message = "Invalid note read\r\n"; break;
                    case 0x16: message = "Invalid note read\r\n"; break;
                    case 0x17: message = "Invalid note read\r\n"; break;
                    case 0x18: message = "Invalid note read\r\n"; break;
                    case 0x19: message = "Incorrect note width\r\n"; break;
                    case 0x1A: message = "Note too short\r\n"; break;
                }
                SystemUtils.logger.SaveDeviceLog(new DeviceLog()
                {
                    DeviceId = "CASH",
                    Cmd = "QueryRejection",
                    Description = message,
                });
            }
        }
    }
}
