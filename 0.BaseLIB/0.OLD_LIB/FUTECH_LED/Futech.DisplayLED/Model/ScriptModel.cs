using Kztek.LedController;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.DisplayLED.Model
{
    internal class ScriptModel
    {
        private List<Row> currentScript;
        private List<Row> defaultScript;
        private int rowNumber = 1;
        public List<Row> CurrentScript { get => currentScript; set => currentScript = value; }
        public List<Row> DefaultScript { get => defaultScript; set => defaultScript = value; }
        public int RowNumber { get => rowNumber; set => rowNumber = value; }
        public int Length { get => CurrentScript.Count;}
        public ScriptModel()
        {
            CurrentScript = new List<Row>();
            DefaultScript = new List<Row>();
        }

        /// <summary>
        /// Thêm 1 dòng lấy cấu hình mặc định đầu tiên
        /// </summary>
        /// <param name="text"></param>
        public void AddLine(string text)
        {
            Row r = new Row()
            {
                Data = text,
                CurrentColor = defaultScript[0].CurrentColor,
                Effect = defaultScript[0].Effect,
                Speed = defaultScript[0].Speed,
                FontSize = defaultScript[0].FontSize,
            };
            currentScript.Add(r);
        }
        /// <summary>
        /// Thêm 1 dòng có cấu hình riêng
        /// </summary>
        /// <param name="row"></param>
        public void AddLine(Row row)
        {
            if (row != null)
            {
                currentScript.Add(row);
            }
        }
        /// <summary>
        /// Thêm 1 dòng có cấu hình tuỳ ý
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Color"></param>
        /// <param name="Fontsize"></param>
        /// <param name="ledEffect"></param>
        /// <param name="EfSpeed"></param>
        public void AddLine(string Text, EM_LedColor Color, int Fontsize = 10, EM_LedEffect ledEffect = EM_LedEffect.Stand, int EfSpeed = 3)
        {
            var r = new Row()
            {
                Speed = EfSpeed,
                FontSize = Fontsize,
                CurrentColor = Color,
                Data = Text,
                Effect = ledEffect
            };
            AddLine(r);
        }
        /// <summary>
        /// Thêm nhiều dòng dùng cấu hình mặc định
        /// </summary>
        /// <param name="datas"></param>
        public void AddLines(List<string> datas)
        {
            int i = 0;
            foreach (string data in datas)
            {
                if(i < rowNumber)
                {
                    if (DefaultScript.Count > i)
                    {
                        Row r = new Row()
                        {
                            Data = data,
                            CurrentColor = defaultScript[i].CurrentColor,
                            Effect = defaultScript[i].Effect,
                            Speed = defaultScript[i].Speed,
                            FontSize = defaultScript[i].FontSize,
                        };
                        AddLine(r);
                    }
                    else
                    {
                        AddLine(data);
                    }
                }
                i++;
            }
        }
        /// <summary>
        /// Thêm nhiều dòng mới với chung 1 cấu hình
        /// </summary>
        /// <param name="datas">dữ liệu</param>
        /// <param name="rowsetting">Cấu hình</param>
        public void AddLines(List<string> datas, Row rowsetting)
        {
            int i = 0;
            foreach (string data in datas)
            {
                if (i < rowNumber)
                {
                    Row r = new Row()
                    {
                        Data = data,
                        CurrentColor = rowsetting.CurrentColor,
                        Effect = rowsetting.Effect,
                        Speed = rowsetting.Speed,
                        FontSize = rowsetting.FontSize,
                    };
                    AddLine(r);
                }
                i++;
            }
        }

        public void CreateDefaultScript(int rownumber, List<Row> data)
        {
            this.rowNumber = rownumber;
            if(data.Count > rownumber)
            {
                DefaultScript.AddRange(data);
            }
            else
            {
                for (int i = 0; i < rowNumber; i++)
                {
                    DefaultScript.Add(data[i]);
                }
            }
        }

        public void ModifyDefaultScript(List<string>data)
        {
            for (int i = 0; i < DefaultScript.Count && i < data.Count; i++)
            {
                DefaultScript[i].Data = data[i];
            }
        }

        public void Clear()
        {
            currentScript.Clear();
        }
    }
}
