namespace IParking.RegisterCustomer
{
    public class ZktecoV5L
    {
        public ControllerInfo ControllerInfo { get; set; }
        private ConcurrentQueue<string> Cmds = new ConcurrentQueue<string>();

        public ConcurrentQueue<RegisterUserControllerRequest> RequestQueues { get; set; } = new ConcurrentQueue<RegisterUserControllerRequest>();
        public AccessMemoryCollection MemoryCollection { get; set; }

        private PushCMDService pushCMDService = new PushCMDService();
        private int timeOut = 30;

        private Timer _timer;

        public event EventSendToServer eventSendToServer;

        public ZktecoV5L(ControllerInfo controllerInfo, int timeOut, AccessMemoryCollection accessMemoryCollection)
        {
            ControllerInfo = controllerInfo;
            this.MemoryCollection = accessMemoryCollection;
            this.timeOut = timeOut;
        }

        public void UpdateStatus()
        {
            _timer?.Dispose(); // huyr timer cu
            ControllerInfo.IsConnect = true;
            // Mỗi 30s gọi ResetStatus
            _timer = new Timer(_ =>
            {
                ControllerInfo.IsConnect = false;
            }, null, TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(30));
        }

        public bool Connect()
        {
            return true;
        }

        public Task PollingStart()
        {
            return null;
        }

        public Task PollingStop()
        {
            _timer?.Dispose();
            return null;
        }

        public async Task<string> GetFinger(string userId, int fingerIndex)
        {
            var fingerDB = tblBiodataOfZktecoPush.GetDataByFields(int.Parse(userId), (int)EmVerifyType.FINGER_PRINT, fingerIndex, ControllerInfo.Id);
            if (fingerDB.Any()) return fingerDB.First();

            long cmd = pushCMDService.GetFinger(long.Parse(userId), fingerIndex, this);

            var result = (int)EmCMDStatus.SENT;
            for (int i = 0; i < timeOut; i++)
            {
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }

                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                await Task.Delay(1000);
            }

            if (result == 1 && ServiceForV5L.EventHandler.biodata.ContainsKey(int.Parse(userId)))
            {
                var finger = ServiceForV5L.EventHandler.biodata[int.Parse(userId)].BioData.FirstOrDefault(x => x.Index == fingerIndex && x.Type == EmVerifyType.FINGER_PRINT);
                return finger != null ? finger.Data : null;
            }
            return string.Empty;
        }

        public async Task<string> GetFaceInfo(string userId)
        {
            var face = tblBiodataOfZktecoPush.GetDataByFields(int.Parse(userId), (int)EmVerifyType.FACE_ID, 0, ControllerInfo.Id);
            if (face.Any()) return face.First();
            else return null;
        }

        private async Task<RegisterUserResult> DownloadUser(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);
            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);

            string cardNo = KzHelper.ConvertCardNo(request.AccessKeyCode, request.CardType, CardType.PROXI_C1);
            if (string.IsNullOrEmpty(cardNo))
            {
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = "Sai format thẻ";
                return baseResult;
            }

            var user = new User()
            {
                Pin = validId.UserId,
                Name = request.Customer?.Name ?? string.Empty,
                CardNo = cardNo,
                Password = request.Customer?.Password ?? string.Empty,
                Group = 1
            };
            var key = new AccessControllerMemory
            {
                AccessKeyid = request.AccessKeyId,
                CustomerId = request.GetCustomerId(),
                UserId = validId.UserId,
                AccessKey = new AccessKey
                {
                    Id = request.AccessKeyId,
                    Name = request.AccessKeyName,
                    Code = request.AccessKeyCode,
                    Type = (EmAccessKeyType)request.AccessKeyType
                },
                DeviceId = this.ControllerInfo.Id
            };
            MemoryCollection.Add(key);


            var cmd = pushCMDService.RegisterUser(user, request.TimezoneID, this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(100);
            }

            if (result == 0)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");

                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
                baseResult.Description = $"UserId : {validId.UserId}";
            }
            else
            {

                MemoryCollection.Remove(key);
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public async Task<RegisterUserResult> DeleteUser(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);

            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);
            if (validId.UserIdType == EmValidUserIdType.UNREGISTERED)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
                return baseResult;
            }
            var cmd = pushCMDService.DeleteUser(validId.UserId.ToString(), this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(1000);
            }

            if (result == 0)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
                this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType).UserIdType = EmValidUserIdType.UNREGISTERED;
            }
            else
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public async Task<RegisterUserResult> DeleteFinger(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);

            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);
            if (validId.UserIdType == EmValidUserIdType.UNREGISTERED)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
                return baseResult;
            }

            var cmd = pushCMDService.DeleteFingerUser(validId.UserId, request.Index, this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(1000);
            }

            if (result == 0)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
            }
            else
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public async Task<RegisterUserResult> DeleteFace(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);

            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);
            if (validId.UserIdType == EmValidUserIdType.UNREGISTERED)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
                return baseResult;
            }
            var cmd = pushCMDService.DeleteFaceUser(validId.UserId, this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(1000);
            }

            if (result == 0)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
            }
            else
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public async Task<RegisterUserResult> DownloadFace(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);
            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);

            var cmd = pushCMDService.RegisterFaceUser(validId.UserId, request.AccessKeyCode, this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(1000);
            }

            if (result == 0)
            {
                MemoryCollection.Add(new AccessControllerMemory
                {
                    AccessKeyid = request.AccessKeyId,
                    CustomerId = request.GetCustomerId(),
                    UserId = validId.UserId,
                    AccessKey = new AccessKey
                    {
                        Id = request.AccessKeyId,
                        Name = request.AccessKeyName,
                        Code = request.AccessKeyCode,
                        Type = (EmAccessKeyType)request.AccessKeyType
                    },
                    DeviceId = this.ControllerInfo.Id
                });
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");
                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
            }
            else
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public async Task<RegisterUserResult> DownloadFinger(RegisterUserControllerRequest request)
        {
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);
            var validId = this.MemoryCollection.GetValidUserId(request.AccessKeyId, request.GetCustomerId(), request.UserId, (EmAccessKeyType)request.AccessKeyType);

            var cmd = pushCMDService.RegisterFingerUser(validId.UserId, request.Index, request.AccessKeyCode, this);

            var result = (int)EmCMDStatus.SENT;

            for (int i = 0; i < timeOut; i++)
            {
                //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                //{
                //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                //    break;
                //}
                if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                {
                    EventHandler.resultCmd.Remove(cmd);
                    break;
                }
                await Task.Delay(1000);
            }

            if (result == 0)
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.THANH_CONG, "");

                MemoryCollection.Add(new AccessControllerMemory
                {
                    AccessKeyid = request.AccessKeyId,
                    CustomerId = request.GetCustomerId(),
                    UserId = validId.UserId,
                    AccessKey = new AccessKey
                    {
                        Id = request.AccessKeyId,
                        Name = request.AccessKeyName,
                        Code = request.AccessKeyCode,
                        Type = (EmAccessKeyType)request.AccessKeyType
                    },
                    DeviceId = this.ControllerInfo.Id
                });

                baseResult.ResultCode = EmResgisterUserResultCode.SUCCESS;
                baseResult.UserId = validId.UserId;
            }
            else
            {
                tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
                baseResult.ResultCode = EmResgisterUserResultCode.ERROR;
                baseResult.UserId = 0;
                baseResult.Description = $"Code : {result}";
            }

            return baseResult;
        }

        public Tuple<string, string, string> GetTimezone(int timezoneID)
        {
            throw new NotImplementedException();
        }

        public bool GetTimezone(ref string timezoneData, int timezoneID)
        {
            throw new NotImplementedException();
        }

        public async Task<EmCmdResult> SetTimezone(int timezoneID, TimeZone timeZone, int? tryNum = 3)
        {
            if (tryNum == 0) return EmCmdResult.False;
            try
            {
                var cmd = pushCMDService.RegisterTimeZone(timezoneID, timeZone, this);

                var result = (int)EmCMDStatus.SENT;

                for (int i = 0; i < timeOut; i++)
                {
                    //if (!tblCmdOfZktecoPush.HasPendingResultById(cmd))
                    //{
                    //    result = tblCmdOfZktecoPush.GetResultById(cmd) ?? (int)EmCMDStatus.SENT;
                    //    break;
                    //}
                    if (EventHandler.resultCmd.TryGetValue(cmd, out result))
                    {
                        EventHandler.resultCmd.Remove(cmd);
                        break;
                    }
                    await Task.Delay(1000);
                }

                if (result >= 0) return EmCmdResult.Success;
                if (result == (int)EmCMDStatus.SENT || result == (int)EmCMDStatus.NOT_DONE) return EmCmdResult.Timeout;
                else return EmCmdResult.False;
            }
            catch (Exception e)
            {
                return await SetTimezone(timezoneID, timeZone, tryNum - 1);
            }
        }

        public Task<EmCmdResult> DeleteTimezone(int timezoneID, int? tryNum = 3)
        {
            //if (tryNum == 0) return EmCmdResult.False;
            //try
            //{
            //    var cmd = pushCMDService.DeleteTimezone(timezoneID, this);

            //    var result = (int)EmCMDStatus.SENT;

            //    for (int i = 0; i < timeOut; i++)
            //    {
            //        if (!(sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return == (int)EmCMDStatus.SENT || sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return == (int)EmCMDStatus.NOT_DONE))
            //        {
            //            result = sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return;
            //            break;
            //        }
            //        await Task.Delay(1000);
            //    }

            //    if (result >= 0) return EmCmdResult.Success;
            //    if (result == (int)EmCMDStatus.SENT || result == (int)EmCMDStatus.NOT_DONE) return EmCmdResult.Timeout;
            //    else return EmCmdResult.False;
            //}
            //catch (Exception e)
            //{
            //    return await DeleteTimezone(timezoneID, tryNum - 1);
            //}

            return null;
        }

        public string GetDateTime()
        {
            return null;
        }

        public Task<EmCmdResult> SetDateTime(string dateTime, int? tryNum = 3)
        {
            return SetDateTime(DateTime.Parse(dateTime), tryNum);
        }

        public Task<EmCmdResult> SetDateTime(DateTime dateTime, int? tryNum = 3)
        {
            //if (tryNum == 0) return EmCmdResult.False;
            //try
            //{
            //    var cmd = pushCMDService.SetTime(dateTime, this);

            //    var result = (int)EmCMDStatus.SENT;

            //    for (int i = 0; i < timeOut; i++)
            //    {
            //        if (!(sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return == (int)EmCMDStatus.SENT || sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return == (int)EmCMDStatus.NOT_DONE))
            //        {
            //            result = sqlLite.GetLogCmd().Where(x => x.ID == cmd).First().Return;
            //            break;
            //        }
            //        await Task.Delay(1000);
            //    }
            //    if (result >= 0) return EmCmdResult.Success;
            //    if (result == (int)EmCMDStatus.SENT || result == (int)EmCMDStatus.NOT_DONE) return EmCmdResult.Timeout;
            //    else return EmCmdResult.False;
            //}
            //catch (Exception e)
            //{
            //    return await SetDateTime(dateTime, tryNum - 1);
            //}

            return null;
        }

        public async Task<bool> SyncTime()
        {
            var result = await SetDateTime(DateTime.Now);
            if (result == EmCmdResult.Success) return true;
            else return false;
        }

        public bool SetRelayDelayTime(int option)
        {
            throw new NotImplementedException();
        }

        public bool Open(int outputNo, int option)
        {
            throw new NotImplementedException();
        }

        public bool Open(int[] outputs)
        {
            throw new NotImplementedException();
        }

        public bool Open(string outputs)
        {
            throw new NotImplementedException();
        }

        public bool Close(int outputNo, int option)
        {
            throw new NotImplementedException();
        }

        public bool Close(int[] outputs)
        {
            throw new NotImplementedException();
        }

        public bool Close(string outputs)
        {
            throw new NotImplementedException();
        }

        public string GetCMD()
        {
            Cmds.TryDequeue(out var cmd);
            return string.IsNullOrEmpty(cmd) ? "OK" : cmd;
        }

        public void AddCMD(string cmd)
        {
            Cmds.Enqueue(cmd);
        }

        public bool Reboot()
        {
            return false;
        }

        public async Task<RegisterUserResult> RegisterUser(RegisterUserControllerRequest request)
        {
            var actionType = request.ActionType;
            var accessKeyType = request.AccessKeyType;
            var baseResult = new RegisterUserResult(request, this.ControllerInfo.Id);

            tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.DANG_THUC_HIEN, "");

            if (actionType == ActionTypeEnum.CREATED)
            {
                switch (accessKeyType)
                {
                    case EmVerifyType.FACE_ID:
                        return await DownloadFace(request);
                    case EmVerifyType.FINGER_PRINT:
                        return await DownloadFinger(request);
                    case EmVerifyType.CARD:
                        return await DownloadUser(request);
                    default:
                        baseResult.ResultCode = EmResgisterUserResultCode.DEVICE_NOT_SUPPORT;
                        return baseResult;
                }
            }
            if (actionType == ActionTypeEnum.DELETED)
            {
                switch (accessKeyType)
                {
                    case EmVerifyType.FACE_ID:
                        return await DeleteFace(request);
                    case EmVerifyType.FINGER_PRINT:
                        return await DeleteFinger(request);
                    case EmVerifyType.CARD:
                        return await DeleteUser(request);
                    default:
                        baseResult.ResultCode = EmResgisterUserResultCode.DEVICE_NOT_SUPPORT;
                        return baseResult;
                }
            }
            tblLogCMD.UpdateStatusByCmdID(request.CmdId, (int)EmCmdStatus.LOI, "");
            baseResult.ResultCode = EmResgisterUserResultCode.DEVICE_NOT_SUPPORT;
            return baseResult;

        }

        public void AddCMDFromServer(RegisterUserControllerRequest data)
        {
            RequestQueues.Enqueue(data);
            PushCMDService.IsHaveNewCMD = true;
        }

        public RegisterUserControllerRequest GetCMDFromServer()
        {
            RequestQueues.TryDequeue(out var cmd);
            return cmd;
        }

        public List<RegisterUserControllerRequest> GetAllCMDFromServer()
        {
            List<RegisterUserControllerRequest> result = new List<RegisterUserControllerRequest>();
            result = RequestQueues.ToList();
            return result;
        }

        public List<EmployeeModel> GetAllUser()
        {
            throw new NotImplementedException();
        }
    }

}
