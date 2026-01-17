using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class User
    {
        // Constructor
        public User()
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

        // Login property
        private string login = "";
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        // Password property
        private string password = "";
        public string Password
        {
            get { return password; }
            set { password = value; }
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
