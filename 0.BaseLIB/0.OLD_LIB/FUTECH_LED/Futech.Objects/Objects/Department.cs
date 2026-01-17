using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Department
    {
        // Constructor
        public Department()
        {

        }

        // ID property
        private int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        // Code property
        private string code = "";
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        // Name property
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Description property
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        // DeviceTotal property
        private int devicetotal = 0;
        public int DeviceTotal
        {
            get { return devicetotal; }
            set { devicetotal = value; }
        }
    }
}
