using iParkingv5.LedDisplay.LEDs;
using System;
using System.Collections.Generic;
using System.Text;
using static iParkingv5.LedDisplay.Enums.LedColors;

namespace iParkingv5.LedDisplay.Enums
{
    public class LineConfig
    {
        private const int _MinSpeed = 1;
        private const int _MaxSpeed = 10;
        private const int _DefaultSpeed = 5;

        private EmLedEffect effect;
        public EmLedEffect Effect { get; set; }

        private int speed;
        public int Speed
        {
            get { return speed; }
            set
            {
                if (value >= _MinSpeed && value <= _MaxSpeed)
                {
                    speed = value;
                }
                else
                {
                    speed = _DefaultSpeed;
                }
            }
        }

        private int fontSize = (int)EmFontSize.FontSize_10;
        public int FontSize { get => fontSize; set => fontSize = value; }

        private EmLedColor color = EmLedColor.RED;
        public EmLedColor DisplayColor
        {
            get => color;
            set => color = value;
        }

        private EmLedDisplayValue displayValue;
        public EmLedDisplayValue DisplayValue { get => displayValue; set => displayValue = value; }

        private string _data = string.Empty;
        public string Data { get => _data; set => _data = value; }

        public List<Tuple<EmLedColor, string>> MultyColorDatas = new List<Tuple<EmLedColor, string>>();
        public static LineConfig CloneData(LineConfig data)
        {
            EmLedEffect effect = data.Effect;
            int speed = data.Speed;
            int fontsize = data.FontSize;
            EmLedColor color = data.DisplayColor;
            string displayText = data.Data;
            List<Tuple<EmLedColor, string>> MultyColorDatas = new List<Tuple<EmLedColor, string>>();
            return new LineConfig()
            {
                Effect = effect,
                Speed = speed,
                FontSize = fontsize,
                DisplayColor = color,
                Data = displayText,
            };
        }
    }
}
