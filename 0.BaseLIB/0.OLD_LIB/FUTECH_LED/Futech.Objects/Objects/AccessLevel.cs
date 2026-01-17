using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class AccessLevel
    {
        // Constructor
        public AccessLevel()
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

        // Description property
        private string description = "";
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
