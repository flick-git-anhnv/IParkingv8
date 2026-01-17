namespace iParkingv5.Controller.Dahua
{
    //public class DahuaAccessControl : IController
    //{
    //    public Dictionary<int, EmCardDispenserStatus> DispensersStatus { get; set; } = new Dictionary<int, EmCardDispenserStatus>();
    //    public CardDispenserStatus cardDispenserStatus { get; set; }
    //    private IntPtr m_LoginID;
    //    private bool m_IsListen = false;
    //    private static fDisConnectCallBack? m_DisConnectCallBack;//断线回调
    //    private static fHaveReConnectCallBack? m_ReConnectCallBack;//重连回调
    //    public fMessCallBackEx? m_AlarmCallBack; //报警回调
    //    public event CancelEventHandler? CancelEvent;

    //    const string TimeFormat = "yyyyMMddHHmmss";

    //    public Bdk ControllerInfo { get; set; }
    //    private string id;
    //    public DahuaAccessControl(string id)
    //    {
    //        m_DisConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
    //        m_ReConnectCallBack = new fHaveReConnectCallBack(ReConnectCallBack);
    //        m_AlarmCallBack = new fMessCallBackEx(AlarmCallBack);
    //        this.id = id;
    //    }
    //    #region Event
    //    public event CardEventHandler? CardEvent;
    //    public event FingerEventHandler? FingerEvent;
    //    public event ControllerErrorEventHandler? ErrorEvent;
    //    public event InputEventHandler? InputEvent;
    //    public event ConnectStatusChangeEventHandler? ConnectStatusChangeEvent;
    //    public event DeviceInfoChangeEventHandler? DeviceInfoChangeEvent;
    //    public void PollingStart()
    //    {
    //        NETClient.SetDVRMessCallBack(m_AlarmCallBack, IntPtr.Zero);

    //        bool ret = NETClient.StartListen(m_LoginID);
    //        m_IsListen = true;

    //    }
    //    public void PollingStop()
    //    {
    //        bool ret = NETClient.StopListen(m_LoginID);
    //    }
    //    public void DeleteCardEvent()
    //    {
    //    }

    //    public async Task<bool> OpenDoor(int timeInMilisecond, int relayIndex)
    //    {
    //        return false;
    //    }
    //    #endregion End Event

    //    #region: CONNECT
    //    public async Task<bool> TestConnectionAsync()
    //    {
    //        if (CommunicationType.IS_TCP((EmCommunicationType)(this.ControllerInfo.CommunicationType)))
    //        {
    //            if (NetWorkTools.IsPingSuccess(this.ControllerInfo.Comport, 500))
    //            {
    //                NET_DEVICEINFO_Ex deviceInfo = new NET_DEVICEINFO_Ex();
    //                string ip = this.ControllerInfo.Comport;
    //                ushort port = ushort.Parse(this.ControllerInfo.Baudrate);
    //                string username = "admin";
    //                string password = "admin";
    //                m_LoginID = NETClient.LoginWithHighLevelSecurity(ip, port, username, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref deviceInfo);

    //                this.ControllerInfo.IsConnect = m_LoginID != IntPtr.Zero;
    //                return this.ControllerInfo.IsConnect;
    //            }
    //        }
    //        return false;
    //    }
    //    public async Task<bool> ConnectAsync()
    //    {
    //        return await this.TestConnectionAsync();
    //    }
    //    public async Task<bool> DisconnectAsync()
    //    {
    //        if (m_IsListen)
    //        {
    //            NETClient.StopListen(m_LoginID);
    //        }
    //        if (IntPtr.Zero != m_LoginID)
    //        {
    //            NETClient.Logout(m_LoginID);
    //        }

    //        m_LoginID = IntPtr.Zero;
    //        return true;
    //    }
    //    #endregion: END CONNECT

    //    #region DATE - TIME
    //    public async Task<DateTime> GetDateTime()
    //    {
    //        return DateTime.MinValue;
    //    }
    //    public async Task<bool> SetDateTime(DateTime time)
    //    {
    //        return false;
    //    }
    //    public async Task<bool> SyncDateTime()
    //    {
    //        return await SetDateTime(DateTime.Now);
    //    }
    //    #endregion END DATE - TIME

    //    #region:TCP_IP
    //    //GET
    //    public async Task<string> GetIPAsync()
    //    {
    //        return string.Empty;
    //    }
    //    public async Task<string> GetMacAsync()
    //    {
    //        return string.Empty;
    //    }
    //    public async Task<string> GetDefaultGatewayAsync()
    //    {
    //        return string.Empty;
    //    }

    //    public async Task<int> GetPortAsync()
    //    {
    //        return 37777;
    //    }
    //    public async Task<string> GetComkeyAsync()
    //    {
    //        return string.Empty;
    //    }
    //    //SET
    //    public async Task<bool> SetMacAsync(string macAddr)
    //    {
    //        return false;
    //    }
    //    public async Task<bool> SetNetWorkInforAsync(string ip, string subnetMask, string defaultGateway, string macAddr)
    //    {
    //        return false;
    //    }
    //    public async Task<bool> SetComKeyAsync(string comKey)
    //    {
    //        return false;
    //    }
    //    #endregion: END TCP_IP

    //    #region System
    //    public async Task<bool> ClearMemory()
    //    {
    //        return true;
    //    }
    //    public async Task<bool> RestartDevice()
    //    {
    //        return true;
    //    }
    //    public async Task<bool> ResetDefault()
    //    {
    //        return true;
    //    }
    //    #endregion End System

    //    #region CallBack 回调
    //    public static void Init()
    //    {
    //        try
    //        {
    //            NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);
    //            NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);

    //        }
    //        catch (Exception ex)
    //        {
    //            MessageBox.Show(ex.Message);
    //            Process.GetCurrentProcess().Kill();
    //        }
    //    }
    //    private void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
    //    {
    //        this.ControllerInfo.IsConnect = false;
    //    }
    //    private void ReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
    //    {
    //        this.ControllerInfo.IsConnect = true;
    //    }
    //    private bool AlarmCallBack(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, string pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
    //    {
    //        EM_ALARM_TYPE type = (EM_ALARM_TYPE)lCommand;
    //        switch (type)
    //        {
    //            case EM_ALARM_TYPE.ALARM_ACCESS_CTL_EVENT:
    //                NET_ALARM_ACCESS_CTL_EVENT_INFO access_info = (NET_ALARM_ACCESS_CTL_EVENT_INFO)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_ACCESS_CTL_EVENT_INFO));

    //                if (access_info.emOpenMethod == EM_ACCESS_DOOROPEN_METHOD.FINGERPRINT)
    //                {
    //                    int userId = 0;
    //                    try
    //                    {
    //                        userId = int.Parse(Encoding.Default.GetString(access_info.szUserID));
    //                        if (userId > 0)
    //                        {
    //                            CallFingerEvent(pchDVRIP, userId, 1);
    //                        }
    //                    }
    //                    catch (Exception)
    //                    {
    //                        CallFingerEvent(pchDVRIP, 0, 1);
    //                    }
    //                }
    //                else
    //                {
    //                    string cardNumber = access_info.szCardNo.ToString();
    //                    CallCardEvent(pchDVRIP, cardNumber, 1);
    //                }

    //                break;
    //            default:
    //                break;
    //        }

    //        return true;
    //    }
    //    private void CallCardEvent(string ip, string cardNumber, int readerIndex)
    //    {
    //        CardEventArgs e = new CardEventArgs
    //        {
    //            DeviceId = ip,
    //            AllCardFormats = new List<string>(),
    //        };
    //        string cardNumberHEX = cardNumber;
    //        if (!string.IsNullOrEmpty(cardNumberHEX))
    //        {
    //            e.AllCardFormats.Add(cardNumberHEX);

    //            if (cardNumberHEX.Length == 6)
    //            {
    //                string maTruocToiGian = long.Parse(cardNumberHEX, NumberStyles.HexNumber).ToString();
    //                string maTruocFull = Convert.ToInt64(cardNumberHEX, 16).ToString("0000000000");

    //                string maSauFormat1 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString("000") +
    //                                      int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("00000");

    //                string maSauFormat2 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString("000") + ":" +
    //                                      int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("00000");

    //                string maSauFormat3 = int.Parse(cardNumberHEX.Substring(0, 2), NumberStyles.HexNumber).ToString() + ":" +
    //                  int.Parse(cardNumberHEX.Substring(2, 4), NumberStyles.HexNumber).ToString("");

    //                e.PreferCard = maSauFormat3;

    //                e.AllCardFormats.Add(maTruocToiGian);
    //                if (maTruocToiGian != maTruocFull)
    //                {
    //                    e.AllCardFormats.Add(maTruocFull);
    //                }
    //                e.AllCardFormats.Add(maSauFormat1);
    //                e.AllCardFormats.Add(maSauFormat2);
    //            }
    //            else
    //            {

    //                string temp = cardNumberHEX.Substring(6, 2) + cardNumberHEX.Substring(4, 2) + cardNumberHEX.Substring(2, 2) + cardNumberHEX.Substring(0, 2);

    //                string maInt = Convert.ToInt64(temp, 16).ToString();
    //                while (maInt.Length<10)
    //                {
    //                    maInt = "0" + maInt;
    //                }
    //                e.PreferCard = maInt;
    //                e.AllCardFormats.Add(maInt);
    //            }
    //        }
    //        //string str_readerIndex = map.ContainsKey("reader") ? map["reader"] : "";
    //        e.ReaderIndex = readerIndex;
    //        this.CardEvent?.Invoke(this, e);
    //    }
    //    private void CallFingerEvent(string ip, int userId, int readerIndex)
    //    {
    //        FingerEventArgs e = new FingerEventArgs
    //        {
    //            DeviceId = ip,
    //            UserId = userId.ToString(),
    //            ReaderIndex = readerIndex
    //        };
    //        this.FingerEvent?.Invoke(this, e);
    //    }

    //    #endregion

    //    #region USER
    //    public async Task<bool> AddFinger(List<string> fingerDatas, string customerName, int userId)
    //    {
    //        RegisterCustomer(customerName, userId, out bool result, out NET_EM_FAILCODE[] stuOutErrArray);
    //        if (!result)
    //        {
    //            return false;
    //        }

    //        //Xóa toàn bộ thông tin vân tay cũ
    //        result = false;
    //        string[] userid = new string[] { userId.ToString() };
    //        result = NETClient.RemoveOperateAccessFingerprintService(m_LoginID, userid, out stuOutErrArray, 5000);

    //        //Đăng ký vân tay mới
    //        bool isAllSuccess = true;
    //        for (int i = 0; i < fingerDatas.Count; i++)
    //        {
    //            isAllSuccess = AssignFinger(fingerDatas, isAllSuccess, i + 1, userId);
    //        }
    //        return isAllSuccess;
    //    }
    //    public async Task<bool> ModifyFinger(List<string> fingerDatas, string customerName, int userId)
    //    {
    //        return false;
    //    }
    //    public async Task<bool> DeleteFinger(string userId, int fingerIndex)
    //    {
    //        NET_EM_FAILCODE[] stuOutErrArray = new NET_EM_FAILCODE[1];
    //        string[] InUserid = new string[] { userId };
    //        bool result = NETClient.RemoveOperateAccessUserService(m_LoginID, InUserid, out stuOutErrArray, 5000);
    //        var lastError = NETClient.GetLastError();
    //        return result;
    //    }
    //    #endregion END USER

    //    #region Private Function
    //    private bool AssignFinger(List<string> fingerDatas, bool isAllSuccess, int i, int userId)
    //    {
    //        int m_PacketLen = 810;
    //        List<string> tempList = fingerDatas[i - 1].Split(" ").ToList();
    //        byte[] FingerPrintInfo = new byte[tempList.Count];
    //        for (int j = 0; j < tempList.Count; j++)
    //        {
    //            FingerPrintInfo[j] = (byte)int.Parse(tempList[j]);
    //        }
    //        NET_ACCESS_FINGERPRINT_INFO fingerprint_info = new NET_ACCESS_FINGERPRINT_INFO();
    //        fingerprint_info.nPacketNum = 1;
    //        fingerprint_info.nPacketLen = m_PacketLen;
    //        fingerprint_info.szFingerPrintInfo = Marshal.AllocHGlobal(m_PacketLen);
    //        fingerprint_info.nDuressIndex = 0;
    //        fingerprint_info.szUserID = userId.ToString();
    //        for (int j = 0; j < m_PacketLen; j++)
    //        {
    //            Marshal.WriteByte(fingerprint_info.szFingerPrintInfo, j, FingerPrintInfo[j]);
    //        }
    //        NET_ACCESS_FINGERPRINT_INFO[] stuFingerInArray = new NET_ACCESS_FINGERPRINT_INFO[1] { fingerprint_info };
    //        NET_EM_FAILCODE[] stuOutArray;

    //        bool bRet = NETClient.InsertOperateAccessFingerprintService(m_LoginID, stuFingerInArray, out stuOutArray, 5000);
    //        if (!bRet)
    //        {
    //            isAllSuccess = false;
    //        }

    //        return isAllSuccess;
    //    }
    //    private void RegisterCustomer(string customerName, int userId, out bool result, out NET_EM_FAILCODE[] stuOutErrArray)
    //    {
    //        NET_ACCESS_USER_INFO userInfo = new NET_ACCESS_USER_INFO
    //        {
    //            szUserID = userId.ToString(),
    //            szName = customerName,
    //            szPsw = "",
    //            emAuthority = EM_ATTENDANCE_AUTHORITY.Customer,

    //            nTimeSectionNum = 1,
    //            nTimeSectionNo = new int[32]
    //        };
    //        userInfo.nTimeSectionNo[0] = 0;

    //        userInfo.nSpecialDaysScheduleNum = 1;
    //        userInfo.nSpecialDaysSchedule = new int[128];
    //        userInfo.nSpecialDaysSchedule[0] = 0;

    //        userInfo.emUserType = EM_USER_TYPE.NORMAL;
    //        userInfo.nUserTime = 0;
    //        userInfo.bFirstEnter = false;
    //        userInfo.nFirstEnterDoorsNum = 0;

    //        userInfo.stuValidStartTime = NET_TIME.FromDateTime(DateTime.Now);
    //        userInfo.stuValidEndTime = NET_TIME.FromDateTime(DateTime.Now.AddYears(1));
    //        userInfo.nDoors = new int[32];
    //        userInfo.nDoors[0] = 0;
    //        userInfo.nDoorNum = 1;

    //        result = false;
    //        NET_ACCESS_USER_INFO[] stuInArray = new NET_ACCESS_USER_INFO[1] { userInfo };
    //        stuOutErrArray = new NET_EM_FAILCODE[1];
    //        result = NETClient.InsertOperateAccessUserService(m_LoginID, stuInArray, out stuOutErrArray, 5000);
    //        var a = NETClient.GetLastError();
    //    }

    //    public Task<bool> CollectCard()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> DispenseCard()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Task<bool> RejectCard()
    //    {
    //        throw new NotImplementedException();
    //    }
    //    #endregion End Private Function
    //}
}