using System;
using System.Collections.Generic;

namespace Kztek.Object
{
    public class Employee
    {
        public int MemoryID { get; set; } = 0;
        public string CardNumber { get; set; } = "";
        public int CardType { get; set; } = (int)EmCardType.Mifare;
        public int TimezoneID { get; set; } = 0;
        public List<string> fingerDatas { get; set; } = new List<string>();
        public string Name { get; set; } = "";
        private string password = string.Empty;
        public List<int> Doors { get; set; } = new List<int>();
        public string Password
        {
            get { return this.password; }
            set
            {
                if (value.Length > 8)
                    throw new ArgumentException("Password length must be smaller then 8 characters");
                else
                    this.password = value;
            }
        }
    }

}
