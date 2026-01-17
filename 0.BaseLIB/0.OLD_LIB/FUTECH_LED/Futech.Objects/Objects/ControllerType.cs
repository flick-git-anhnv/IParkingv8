using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class ControllerType
    {
        // Constructor
        public ControllerType()
        {
 
        }

        // ID property
        private int id = 0;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        // Name property
        private string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // LineTypeID
        private int lineTypeID = 0;
        public int LineTypeID
        {
            get { return lineTypeID; }
            set { lineTypeID = value; }
        }

        // Parent property
        private LineType parent = null;
        public LineType Parent
        {
            get { return parent; }
            set { parent = value; }
        }
    }
}
