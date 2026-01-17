using Kztek.Object.Entity.Device.ControllerModels;
using System;

namespace Kztek.Object
{
    public class Events
    {
        public delegate void OnErrorEventHandler(Controller controller, ErrorEventArgs e);
        public class ErrorEventArgs : EventArgs
        {
            public string ErrorString { get; set; } = string.Empty;
            public string ErrorFunc { get; set; } = string.Empty;
            public string ErrorLine { get; set; } = string.Empty;
            public string CMD { get; set; } = string.Empty;
        }

        #region Connect
        public delegate void ConnectStatusChangeEventHandler(Controller controller, ConnectStatusCHangeEventArgs e);
        public class ConnectStatusCHangeEventArgs : EventArgs
        {
            public bool CurrentStatus { get; set; }
            public string reason { get; set; } = string.Empty;
        }
        #endregion

        #region Employee
        public delegate void OnDownloadEmployee(Controller controller, EmployeeEventArgs e);
        public delegate void OnDeleteEmployee(Controller controller, EmployeeEventArgs e);

        public class EmployeeEventArgs : EventArgs
        {
            public EmEmployeeAction Action { get; set; } = EmEmployeeAction.Add;
            public DateTime EventTime { get; set; }
            public Staff Input { get; set; } = new Staff();
            public EmployeeResult Output { get; set; } = new EmployeeResult();
        }

        public class EmployeeResult
        {
            public string ProvidedId { get; set; } = string.Empty;
            public EmEmployeeResult result { get; set; }
            public static string toString(EmEmployeeResult result)
            {
                switch (result)
                {
                    case EmEmployeeResult.OK:
                        return "Thành công";
                    case EmEmployeeResult.DISCONNECT_WITH_DEVICE:
                        return "Mất kết nối tới thiết bị";
                    case EmEmployeeResult.INVALID_ACCESS_LEVEL:
                        return "Không có quyền sử dụng";
                    default:
                        return "Thất Bại";
                }
            }

        }
        public enum EmEmployeeResult
        {
            OK,
            DISCONNECT_WITH_DEVICE,
            INVALID_ACCESS_LEVEL,
            INVALID_DOOR,
            NOT_SUPPORT,
            NOT_REGISTERED,
            ERROR,
            FALSE,
        }
        public enum EmEmployeeAction
        {
            Add,
            Delete
        }
        #endregion

        #region Event
        public delegate void OnEventComeHandler(Controller controller, ControllerEventArgs e);
        public delegate void OnInputEventHandler(Controller controller, InputEventArgs e);
        public class InputEventArgs
        {
            public string ControllerId { get; set; } = string.Empty;
            public string ControllerName { get; set; } = string.Empty;
            public DateTime ControllerEventTime { get; set; }
            public DateTime LocalTime { get; set; }
            public int InputIndex { get; set; }
            public InputState InputState { get; set; } = InputState.Open;
        }
        public class ControllerEventArgs
        {
            public string ProvidedId { get; set; } = string.Empty;
            public string ControllerId { get; set; } = string.Empty;
            public string ControllerName { get; set; } = string.Empty;
            public int IdentityMethod { get; set; }
            public DateTime ControllerEventTime { get; set; }
            public DateTime LocalTime { get; set; }

            public bool IsValidEvent { get; set; }
            public int InputIndex { get; set; }

            public ControllerCardData? CardData { get; set; }
            public ControllerFaceData? FaceData { get; set; }
            public ControllerFingerData? FingerData { get; set; }
        }

        public class ControllerCardData
        {
            public string CardNumber { get; set; } = string.Empty;
        }
        public class ControllerFingerData
        {
            public string FingerId { get; set; } = string.Empty;
        }
        public class ControllerFaceData
        {
            public string FaceId { get; set; } = string.Empty;
        }
        #endregion
    }
}
