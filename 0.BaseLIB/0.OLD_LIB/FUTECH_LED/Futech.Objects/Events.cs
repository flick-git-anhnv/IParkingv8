using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;

namespace Futech.Objects
{
    //The delegate for the event raised.
    public delegate void CardEventHandler(object sender, CardEventArgs e);

    // CardEventArgs
    public class CardEventArgs : EventArgs
    {
        // Constructor
        public CardEventArgs()
        {

        }

        // Line ID
        private int lineID = 0;
        public int LineID
        {
            get { return lineID; }
            set { lineID = value; }
        }
        
        private string faceImage = String.Empty;
        public string FaceImage
        {
            get { return faceImage; }
            set { faceImage = value; }
        }

        // Line Code
        private string lineCode = "";
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        // Controller Address
        private int controllerAddress = 0;
        public int ControllerAddress
        {
            get { return controllerAddress; }
            set { controllerAddress = value; }
        }

        // FID property
        private int fid = 0;
        public int FID
        {
            get { return fid; }
            set { fid = value; }
        }

        // Reader Index
        private int readerIndex = 1;
        public int ReaderIndex
        {
            get { return readerIndex; }
            set { readerIndex = value; }
        }

        // CardNumber property
        private string cardNumber = "";
        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }

        // CardNumber property - 10 so dau - FingerTec
        private string cardNumber10 = "";
        public string CardNumber10
        {
            get { return cardNumber10; }
            set { cardNumber10 = value; }
        }

        // CardNumber property - 8 so sau - IDTECK
        private string cardNumber8 = "";
        public string CardNumber8
        {
            get { return cardNumber8; }
            set { cardNumber8 = value; }
        }

        // Date property, format yyyy/MM/dd
        private string date = "";
        public string Date
        {
            get { return date; }
            set { date = value; }
        }

        // Time property, format HH:mm:ss
        private string time = "";
        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        // Event Status property
        private string eventStatus = "Access Granted";
        public string EventStatus
        {
            get { return eventStatus; }
            set { eventStatus = value; }
        }

        // Function Key
        private string _functionKey = "";
        public string functionKey
        {
            get { return _functionKey; }
            set { _functionKey = value; }
        }

        // Gia tri giao dich
        private int value1 = 0;
        public int Value1
        {
            get { return value1; }
            set { value1 = value; }
        }

        // So du tai khoan
        private int balance = 0;
        public int Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        // Hoat dong INC - ghi tang, DEC - ghi giam
        private string action = "DEC";
        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        // Mieu ta
        private string desc = "";
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        // Ma su kien
        private string eventcode = "02";
        public string EventCode
        {
            get { return eventcode; }
            set { eventcode = value; }
        }

        // ViewRaw
        private string viewraw = "";
        public string ViewRaw
        {
            get { return viewraw; }
            set { viewraw = value; }
        }

        // Message
        private string message = "";
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        // EventID
        private string eventID = "";
        public string EventID
        {
            get { return eventID; }
            set { eventID = value; }
        }

        // IsRecoverEvent
        private bool isRecoverEvent = false;
        public bool IsRecoverEvent
        {
            get { return isRecoverEvent; }
            set { isRecoverEvent = value; }
        }

        // Ma hang (01 -> 50)
        private string productCode = "00";
        public string ProductCode
        {
            get { return productCode; }
            set { productCode = value; }
        }

        private int plan = 0;
        public int Plan
        {
            get { return plan; }
            set { plan = value; }
        }

        private int goal = 0;
        public int Goal
        {
            get { return goal; }
            set { goal = value; }
        }

        private int result = 0;
        public int Result
        {
            get { return result; }
            set { result = value; }
        }

        private string totime = "";
        public string ToTime
        {
            get { return totime; }
            set { totime = value; }
        }

        private string todate = "";
        public string ToDate
        {
            get { return todate; }
            set { todate = value; }
        }
        private string facilicode = "";
        public string FaciliCode
        {
            get { return facilicode; }
            set { facilicode = value; }
        }

        public Bitmap Bmp1 { get; set; }
        public Bitmap Bmp2 { get; set; }
        public Bitmap Bmp3 { get; set; }
        private bool _isRunoutofCard = false;
        private bool _isAlmostOutOfCard = false;

        public bool IsRunoutofCard { get => _isRunoutofCard; set => _isRunoutofCard = value; }
        public bool IsAlmostOutOfCard { get => _isAlmostOutOfCard; set => _isAlmostOutOfCard = value; }

    }

    public delegate void InputEventHandler(object sender, InputEventArgs e);
    public class InputEventArgs : EventArgs
    {
        //CTO
        public InputEventArgs()
        {
        }
        private string faceImage = String.Empty;
        public string FaceImage
        {
            get { return faceImage; }
            set { faceImage = value; }
        }

        private string boardIndex;
        public string BoardIndex
        {
            get { return boardIndex; }
            set { boardIndex = value; }
        }
        private string eventdate = "";
        public string EventDate
        {
            get { return eventdate; }
            set { eventdate = value; }
        }
        private string eventstatus = "";
        public string EventStatus
        {
            get { return eventstatus; }
            set { eventstatus = value; }
        }

        private string eventTime = "";
        public string EventTime
        {
            get { return eventTime; }
            set { eventTime = value; }
        }

        private string inputport = "";
        public string Inputport
        {
            get { return inputport; }
            set { inputport = value; }
        }

        private short nindex;
        public short nIndex
        {
            get { return nindex; }
            set { nindex = value; }
        }

        private string eventtype = "";
        public string EventType
        {
            get { return eventtype; }
            set { eventtype = value; }
        } 

        // Line ID
        private int lineID = 0;
        public int LineID
        {
            get { return lineID; }
            set { lineID = value; }
        }

        // Line Code
        private string lineCode = "";
        public string LineCode
        {
            get { return lineCode; }
            set { lineCode = value; }
        }

        // Controller Address
        private int controllerAddress = 0;
        public int ControllerAddress
        {
            get { return controllerAddress; }
            set { controllerAddress = value; }
        }

        private string plate = "";
        public string Plate
        {
            get { return plate; }
            set { plate = value; }
        }

        private bool _IsOpenDoor = true;
        public bool IsOpenDoor
        {
            get { return _IsOpenDoor; }
            set { _IsOpenDoor = value; }
        }
        private bool _isRunoutofCard = false;
        private bool _isAlmostOutOfCard = false;

        public bool IsRunoutofCard { get => _isRunoutofCard; set => _isRunoutofCard = value; }
        public bool IsAlmostOutOfCard { get => _isAlmostOutOfCard; set => _isAlmostOutOfCard = value; }
        public Bitmap Bmp1 { get; set; }
        public Bitmap Bmp2 { get; set; }
        public Bitmap Bmp3 { get; set; }
     
    }

    public class ErrorEventArgs : EventArgs
    {
        private string errorString = "";
        public string ErrorString
        {
            get
            {
                return this.errorString;
            }
            set
            {
                this.errorString = value;
            }
        }
        public ErrorEventArgs()
        {
        }
        public ErrorEventArgs(string errorString)
        {
            this.errorString = errorString;
        }
    }

    public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);


}
