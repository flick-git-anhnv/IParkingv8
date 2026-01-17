using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class AccessLevelDetail
    {
        // Constructor
        public AccessLevelDetail()
        {
 
        }

        // AccessLevelID property
        private int accessLevelID = 0;
        public int AccessLevelID
        {
            get { return accessLevelID; }
            set { accessLevelID = value; }
        }

        // ControllerID property
        private int controllerID = 0;
        public int ControllerID
        {
            get { return controllerID; }
            set { controllerID= value; }
        }

        // TimezoneID property
        private int timezoneID = 0;
        public int TimezoneID
        {
            get { return timezoneID; }
            set { timezoneID = value; }
        }
    }
}
