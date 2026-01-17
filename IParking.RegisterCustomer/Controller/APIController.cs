using Microsoft.AspNetCore.Mvc;
using System.Text;
using static iParkingv5.Controller.ControllerFactory;

namespace IParking.RegisterCustomer.Controller
{
    public class APIController : ControllerBase
    {
        private readonly long SessionId = 5851202300208;
        private readonly long RegistryCode = 5851202300209;

        [HttpGet("api/Health")]
        public ActionResult<string> Hello()
        {
            return new OkObjectResult("OK");
        }
        [HttpPost("iclock/cdata")]
        public async Task<ActionResult<string>> Event([FromQuery] string SN, [FromQuery] string table)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";

            var ControllerCheck = AppData.IControllers.Where(x => x.ControllerInfo.Comport.Equals(clientIp) && x.ControllerInfo.Type == (int)EmControllerType.ZKTECO_PUSH);
            return Ok("OK");

            //ZktecoV5L Controller = null;
            //if (ControllerCheck.Count() == 0)
            //{
            //    return Ok("OK");
            //}
            //else Controller = (ZktecoV5L)ControllerCheck.First();
            //Controller.UpdateStatus();
            //Request.EnableBuffering();
            //using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true))
            //{
            //    string requestBody = await reader.ReadToEndAsync();
            //    Request.Body.Position = 0;
            //    FrmMain.eventHandler.PushEvent(requestBody, table, SN, AppData.Customers, Controller);
            //    return new OkObjectResult(message);
            //}
        }
        [HttpPost("/iclock/registry")]
        public async Task<ActionResult<string>> Registry([FromQuery] string SN)
        {
            string message = $"RegistryCode={RegistryCode}";
            await Task.Delay(1000);
            return new OkObjectResult(message);
        }

        [HttpPost("/iclock/push")]
        public ActionResult<string> DownloadConfiguration([FromQuery] string SN)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";

            var ControllerCheck = AppData.IControllers.Where(x => x.ControllerInfo.configs.Equals(clientIp) && x.ControllerInfo.Type == (int)EmControllerType.ZKTECO_PUSH);
            //ZktecoV5L Controller = null;
            //if (ControllerCheck.Count() == 0)
            //{
            //    return Ok("OK");
            //}
            //else Controller = (ZktecoV5L)ControllerCheck.First();
            //Controller.UpdateStatus();
            string message = "ServerVersion=1\r\nPushVersion=3.1.1";
            return new OkObjectResult(message);
        }

        [HttpGet("/iclock/getrequest")]
        public ActionResult<string> GetRequest([FromQuery] string SN)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";
            var ControllerCheck = AppData.IControllers.Where(x => x.ControllerInfo.configs.Equals(clientIp) && x.ControllerInfo.Type == (int)EmControllerType.ZKTECO_PUSH);
            ZktecoV5L Controller = null;
            if (ControllerCheck.Count() == 0)
            {
                return new OkObjectResult("OK");
            }
            else Controller = (ZktecoV5L)ControllerCheck.First();
            Controller.UpdateStatus();

            string message = Controller.GetCMD();
            if (!message.Equals("OK"))
            {
                var logcmdcheck = tblCmdOfZktecoPush.GetByCmd(message);
                if (logcmdcheck != null)
                {
                    tblCmdOfZktecoPush.UpdateResultById(logcmdcheck.ID, (int)EmCMDStatus.SENT);
                    PushCMDService.IsHaveNewCMD = true;
                }

            }
            return new OkObjectResult(message);
        }

        [HttpPost("iclock/devicecmd")]
        public async Task<ActionResult<string>> DeviceCMD([FromQuery] string SN)
        {
            string clientIp = HttpContext.Connection.RemoteIpAddress?.MapToIPv4()?.ToString() ?? "";

            var ControllerCheck = AppData.IControllers.Where(x => x.ControllerInfo.Ip.Equals(clientIp) && x.ControllerInfo.ControllerType == EmControllerType.ZKTECO_PUSH);
            ZktecoV5L Controller = null;
            if (ControllerCheck.Count() == 0)
            {
                return new OkObjectResult("OK");
            }
            else Controller = (ZktecoV5L)ControllerCheck.First();
            Controller.UpdateStatus();

            Request.EnableBuffering();
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                string requestBody = await reader.ReadToEndAsync();
                Request.Body.Position = 0;
                FrmMain.eventHandler.PushEvent(requestBody, "", SN, AppData.Customers, Controller);
                return new OkObjectResult("OK");
            }
        }

        [HttpGet("iclock/ping")]
        public ActionResult<string> Ping([FromQuery] string SN)
        {
            var ControllerCheck = AppData.IControllers.Where(x => x.ControllerInfo.Ip.Equals(clientIp) && x.ControllerInfo.ControllerType == EmControllerType.ZKTECO_PUSH);
            ZktecoV5L Controller = null;
            if (ControllerCheck.Count() == 0)
            {
                return Ok("OK");
            }
            else Controller = (ZktecoV5L)ControllerCheck.First();
            Controller.UpdateStatus();

            string message = $"registry=ok\r\nRegistryCode={RegistryCode}\r\nSessionId={SessionId}";
            return Ok("OK");
        }
    }
}
