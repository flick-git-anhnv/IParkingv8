using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Kztek.Tool
{
    public class NetWorkTools
    {
        public static async Task<bool> IsPingSuccessAsync(string ipAddress, int timeOut)
        {
            Ping pingSender = new Ping();
            PingReply reply = await pingSender.SendPingAsync(ipAddress, timeOut);
            if (reply != null && reply.Status == IPStatus.Success)
                return true;
            else
            {
                reply = await pingSender.SendPingAsync(ipAddress, timeOut);
                if (reply != null && reply.Status == IPStatus.Success)
                    return true;
            }
            return false;
        }

        public static bool IsPingSuccess(string ipAddress, int timeOut)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ipAddress, timeOut);
            if (reply != null && reply.Status == IPStatus.Success)
                return true;

            reply = pingSender.Send(ipAddress, timeOut);
            if (reply != null && reply.Status == IPStatus.Success)
                return true;
            return false;
        }

        public static List<string> GetLocalIPAddress()
        {
            List<string> ips = new List<string>();

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ips.Add(ip.ToString());
                }
            }
            return ips;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
