using Futech.DisplayLED.LedDirection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.DisplayLED
{
    public class KztekDirectionLed : IDisplayLED
    {
        #region Properties
        private int _RowNumber = 1;
        private int _DisplayDuration = 1000;
        private string _IPAddress;
        public string IPAddress { get => _IPAddress; set => _IPAddress = value; }

        public int DisplayDuration { get => _DisplayDuration; }

        private Task Displaytask;
        private int communicationtype = 0;
        public int CommunicationType
        {
            get { return communicationtype; }
            set { communicationtype = value; }
        }
        private int address = 0;
        public int Address
        {
            get { return address; }
            set { address = value; }
        }

        public int RowNumber { get => _RowNumber; set => _RowNumber = value; }
        KztekDirectionLedHelper kztekDirectionLedHelper;
        #endregion

        public enum EmDirectionState
        {
            ChangeLaneToIn,
            ChangeLaneToOut,
            AccessGrantedIn,
            AccessGrantedOut,
            AccessDenied,
        }

        public void Close()
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }

        public bool Connect(string comport, int baudrate)
        {
            kztekDirectionLedHelper = new KztekDirectionLedHelper();
            return kztekDirectionLedHelper.Connect(comport, baudrate);
        }

        public void SendToLED(string dtimein, string dtimeout, string plate, string money, int cardtype, string cardstate)
        {
            if(cardstate == EmDirectionState.ChangeLaneToIn.ToString())
            {
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.LeftToRight_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Stand, 0);
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.LeftToRight_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Stand, 1);

            }
            else if(cardstate == EmDirectionState.ChangeLaneToOut.ToString())
            {
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.RightToLeft_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Stand, 0);
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.RightToLeft_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Stand, 1);
            }
            else if (cardstate == EmDirectionState.AccessGrantedIn.ToString())
            {
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.LeftToRight_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 0);
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.LeftToRight_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 1);
            }
            else if (cardstate == EmDirectionState.AccessGrantedOut.ToString())
            {
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.RightToLeft_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 0);
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.RightToLeft_horizontalArrow, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 1);
            }
            else if (cardstate == EmDirectionState.AccessDenied.ToString())
            {
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.Red_X_Character, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 0);
                kztekDirectionLedHelper.SetScreenCurrent(KztekDirectionLedHelper.EmDirectionLedScreenMode.Red_X_Character, KztekDirectionLedHelper.EmDirectionLedEffect.Blink, 1);
            }
        }

        public void SetParkingSlot(int slotNo, byte arrow, byte color)
        {
        }
    }
}
