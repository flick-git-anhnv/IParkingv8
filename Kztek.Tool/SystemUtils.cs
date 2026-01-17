using Kztek.Object;
using Kztek.Tool.LogHelpers.LocalData;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Net;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace Kztek.Tool
{
    public class SystemUtils
    {
        public struct SystemTime
        {
            public short wYear;

            public short wMonth;

            public short wDayOfWeek;

            public short wDay;

            public short wHour;

            public short wMinute;

            public short wSecond;

            public short wMilliseconds;
        }

        public enum AppBarMessages
        {
            New,
            Remove,
            QueryPos,
            SetPos,
            GetState,
            GetTaskBarPos,
            Activate,
            GetAutoHideBar,
            SetAutoHideBar,
            WindowPosChanged,
            SetState
        }

        public struct APPBARDATA
        {
            public uint cbSize;

            public IntPtr hWnd;

            public uint uCallbackMessage;

            public uint uEdge;

            public Rectangle rc;

            public int lParam;
        }

        public enum AppBarStates
        {
            AutoHide = 1,
            AlwaysOnTop
        }

        public static string programName = "";

        public static string dateSeparator = "/";

        public static string shortDateFormat = "MM/dd/yyyy";

        public static string timeSeparator = ":";

        public static string timeFormat = "HH:mm:ss";

        public static string decimalSymbol = ".";

        public static string thousandSymbol = ",";

        private static string[] strSo = new string[10] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        private static string[] strDonViNho = new string[6] { "linh", "lăm", "mười", "mươi", "mốt", "trăm" };

        private static string[] strDonViLon = new string[4] { "", "nghìn", "triệu", "tỷ" };

        private static string[] strMainGroup;

        private static string[] strSubGroup;

        [RegistryPermission(SecurityAction.LinkDemand, Write = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run")]
        public static bool SetProgramStartWithWindows(string programName, bool autoStartWithWindows, string excecutePath)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
                if (autoStartWithWindows)
                {
                    registryKey.SetValue(programName, "\"" + excecutePath + "\"");
                }
                else if (registryKey.GetValue(programName) != null)
                {
                    registryKey.DeleteValue(programName);
                }

                registryKey.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [RegistryPermission(SecurityAction.LinkDemand, Write = "HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run")]
        public static bool IsProgramStartWithWindows(string programName)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", writable: true);
                bool result = registryKey.GetValue(programName) != null;
                registryKey.Close();
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsProgramInstalled(string programName)
        {
            try
            {
                string name = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";
                using RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
                string[] subKeyNames = registryKey.GetSubKeyNames();
                foreach (string name2 in subKeyNames)
                {
                    using RegistryKey registryKey2 = registryKey.OpenSubKey(name2);
                    if (registryKey2.GetValue("DisplayName") != null && registryKey2.GetValue("DisplayName").ToString().ToLower()
                        .Contains(programName.ToLower()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return false;
        }

        public static bool IsAdministator()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static string RemoveUnicode(string s)
        {
            string text = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(text[i]) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(text[i]);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static void GetCurrentSystemSettings()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International", writable: true);
                dateSeparator = registryKey.GetValue("sDate").ToString();
                shortDateFormat = registryKey.GetValue("sShortDate").ToString();
                timeSeparator = registryKey.GetValue("sTime").ToString();
                timeFormat = registryKey.GetValue("sTimeFormat").ToString();
                decimalSymbol = registryKey.GetValue("sDecimal").ToString();
                thousandSymbol = registryKey.GetValue("sThousand").ToString();
                registryKey.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void SetCurrentSystemSetting()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International", writable: true);
                registryKey.SetValue("sDate", dateSeparator);
                registryKey.SetValue("sShortDate", shortDateFormat);
                registryKey.SetValue("sTime", timeSeparator);
                registryKey.SetValue("sTimeFormat", timeFormat);
                registryKey.SetValue("sDecimal", decimalSymbol);
                registryKey.SetValue("sThousand", thousandSymbol);
                registryKey.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static void SetStandardSystemSetting()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International", writable: true);
                registryKey.SetValue("sDate", "/");
                registryKey.SetValue("sShortDate", "MM/dd/yyyy");
                registryKey.SetValue("sTime", ":");
                registryKey.SetValue("sTimeFormat", "HH:mm:ss");
                registryKey.SetValue("sDecimal", ".");
                registryKey.SetValue("sThousand", ",");
                registryKey.Close();
            }
            catch (Exception ex)
            {
            }
        }

        public static string GetThisComputerIP()
        {
            List<string> list = new List<string>();
            IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            foreach (IPAddress iPAddress in addressList)
            {
                if (iPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    list.Add(iPAddress.ToString());
                }
            }

            if (list.Count > 0)
            {
                return list[0];
            }

            return "";
        }

        public static bool IsProcessRunning(string processName)
        {
            bool result = false;
            if (Process.GetProcessesByName(processName).Length != 0)
            {
                result = true;
            }

            return result;
        }

        public int GetThreadInUses()
        {
            int workerThreads, completionPortThreads;
            ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);

            int maxWorkerThreads, maxCompletionPortThreads;
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);

            int usedWorkerThreads = maxWorkerThreads - workerThreads;
            return usedWorkerThreads;
            //Console.WriteLine($"🧵 Worker threads in use: {usedWorkerThreads}");
        }
        public static string[] GetAvailableComPorts()
        {
            return SerialPort.GetPortNames();
        }

        public static ILogger logger;
    }
}