using Futech.DisplayLED.SocketHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.DisplayLED.LedDirection
{
    public class KztekDirectionLedHelper
    {
        public enum EmDirectionLedModuleType
        {
            OneSide = 1,
            DoubleSide = 2
        }
        public enum EmDirectionLedScreenMode
        {
            Red_X_Character = 0,
            LeftToRight_horizontalArrow = 1,
            RightToLeft_horizontalArrow = 2,
            BottomToTop_verticalArrow = 3,
            TopToBottom_verticalArrow = 4,
        }
        public enum EmDirectionLedEffect
        {
            Stand = 0,
            Move_NotWordWithXCharacter = 1,
            Blink = 2,
        }
        #region: Properties
        public string IpAddress { get; set; } = string.Empty;
        public int Port { get; set; } = 100;
        public EmDirectionLedModuleType ModuleType { get; set; }
        #endregion: End Properties


        #region: Connect
        public bool Connect(string ip, int port, EmDirectionLedModuleType moduleType = EmDirectionLedModuleType.DoubleSide)
        {
            this.IpAddress = ip;
            this.Port = port;
            this.ModuleType = moduleType;
            return true;
        }

        public bool Disconnect(string ip, int port)
        {
            return true;
        }
        #endregion: End Connect

        #region: System Infor
        public bool GetFirmwareVersion(ref string firmwareVersion, ref string errorMessage)
        {
            string cmd = "";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (string.IsNullOrEmpty(response))
            {
                errorMessage = "EMPTY_RESPONSE";
                return false;
            }
            string[] data = response.Split('/');
            Dictionary<string, string> map = GetEventContent(data);
            firmwareVersion = map.ContainsKey("version") ? map["version"] : "";
            return true;
        }
        #endregion: End System Infor

        public bool SetModuleType(EmDirectionLedModuleType moduleType)
        {
            string cmd = "SetLedModuleType?/ModuleType=" + (int)moduleType;
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        public bool SetScreenResolution(int row, int column, int side = 1)
        {
            string cmd = "";
            if (this.ModuleType == EmDirectionLedModuleType.OneSide)
                cmd = $@"SetScreenResolution?/Row={row}/Col={column}";
            else
                cmd = $@"SetScreenResolution?/LedSide={side}/Row={row}/Col={column}";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        public bool SetBlinkPulse(int timeOnInMilisecond, int timeOffInMilisecond)
        {
            if (timeOnInMilisecond < 100 || timeOnInMilisecond > 2000 || timeOffInMilisecond < 100 || timeOffInMilisecond > 2000)
            {
                throw new Exception("Thời gian thiết lập không nằm trong phạm vi cho phép(100 đến 2000)");
            }
            string cmd = $@"SetBlinkPulse?/TimeOn={timeOnInMilisecond}/TimeOff={timeOffInMilisecond}";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        public bool SetBlinkTimes(int times)
        {
            if (times < 0 || times > 100)
                throw new Exception("Số lần thiết lập không nằm trong phạm vi cho phép(1 đến 100)");
            string cmd = $@"SetBlinkTimes?/Times={times}";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        public bool SetScreenDefault(EmDirectionLedScreenMode mode, int side = 0)
        {
            string cmd = string.Empty;
            if (this.ModuleType == EmDirectionLedModuleType.OneSide)
            {
                cmd = "SetScreenDefault?/ScreenMode=" + (int)mode;
            }
            else
            {
                cmd = $"SetScreenDefault?/LedSide{side}/ScreenMode={(int)mode}";
            }
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        public bool SetScreenCurrent(EmDirectionLedScreenMode mode, EmDirectionLedEffect effect = EmDirectionLedEffect.Stand, int side = 0)
        {
            string cmd = string.Empty;
            if (this.ModuleType == EmDirectionLedModuleType.OneSide)
            {
                cmd = $"SetScreenCurrent?/ScreenMode={(int)mode}/Effect={(int)effect}";
            }
            else
            {
                cmd = $"SetScreenDefault?/LedSide{side}/ScreenMode={(int)mode}/Effect={(int)effect}";
            }
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }

        #region: Network Infor
        public bool GetIpAddr(ref string ipAddress, ref string errorMessage)
        {
            string cmd = "AutoDetect?";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (response != null && response.Length > 0)
            {
                string[] _responses = response.Split('/');
                if (_responses.Length > 1)
                {
                    ipAddress = _responses[1];
                    return true;
                }
            }
            return false;
        }
        public bool GetPort(ref int port, ref string errorMessage)
        {
            port = this.Port;
            return true;
        }
        public bool GetMac(ref string macAddress, ref string errorMessage)
        {
            string cmd = "AutoDetect?";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (response != null && response.Length > 0)
            {
                string[] _responses = response.Split('/');
                if (_responses.Length > 5)
                {
                    macAddress = _responses[5].Substring(0, _responses[5].Length - 1);
                    return true;
                }
            }
            return false;
        }
        public bool GetSubnetMask(ref string subnetMask, ref string errorMessage)
        {
            string cmd = "AutoDetect?";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (response != null && response.Length > 0)
            {
                string[] _responses = response.Split('/');
                if (_responses.Length > 3)
                {
                    subnetMask = _responses[3];
                    return true;
                }
            }
            return false;
        }
        public bool GetGateWay(ref string gateway, ref string errorMessage)
        {
            string cmd = "AutoDetect?";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (response != null && response.Length > 0)
            {
                string[] _responses = response.Split('/');
                if (_responses.Length > 4)
                {
                    gateway = _responses[4];
                    return true;
                }
            }
            return false;
        }
        public bool GetNetworkInfor(ref string ipAddress, ref int port, ref string macAddress, ref string subnetMask, ref string gateway, ref string errorMessage)
        {
            string cmd = "AutoDetect?";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (response != null && response.Length > 0)
            {
                string[] _responses = response.Split('/');
                if (_responses.Length > 5)
                {
                    ipAddress = _responses[1];
                    port = int.Parse(_responses[2]);
                    subnetMask = _responses[3];
                    gateway = _responses[4];
                    macAddress = _responses[5].Substring(0, _responses[5].Length - 1);
                    return true;
                }
            }
            return false;
        }

        public bool ChangeMac(string macAddress)
        {
            string cmd = $"ChangeMacAddress?/Mac={macAddress}";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }
        public bool ChangeNetworkInfor(string ipAddress, int port, string macAddress, string subnetMask, string gateway)
        {
            string cmd = $@"ChangeIP?/IP={ipAddress}/SubnetMask={subnetMask}/DefaultGateWay={gateway}/HostMac={macAddress}/";
            string response = UdpTools.ExecuteCommand_Ascii(this.IpAddress, 100, cmd);
            if (UdpTools.IsSuccess(response, "OK"))
            {
                return true;
            }
            return false;
        }
        #endregion: End Network Infor
        #region: Private
        public static Dictionary<string, string> GetEventContent(string[] datas)
        {
            Dictionary<string, string> output = new Dictionary<string, string>();
            foreach (string data in datas)
            {
                if (data.Contains("="))
                {
                    string[] subData = data.Split('=');
                    output.Add(subData[0].ToLower().Trim(), subData[1].Trim());
                }
            }
            return output;
        }
        #endregion: End Private
    }
}
