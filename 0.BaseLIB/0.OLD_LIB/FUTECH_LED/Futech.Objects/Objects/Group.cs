using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Group
    {
        // Constructor
        public Group()
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

        // DepartmentID property
        private int departmentID = 0;
        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

        // Description property
        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
