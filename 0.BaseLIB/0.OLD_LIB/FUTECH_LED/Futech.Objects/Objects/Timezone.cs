using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Timezone
    {
        // Constructor
        public Timezone()
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
        private string name = "Timezone1";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Monday property
        private string mon = "00:00-00:00";
        public string Mon
        {
            get { return mon; }
            set { mon = value; }
        }

        // Tuesday property
        private string tue = "00:00-00:00";
        public string Tue
        {
            get { return tue; }
            set { tue = value; }
        }

        // Wednesday property
        private string wed = "00:00-00:00";
        public string Wed
        {
            get { return wed; }
            set { wed = value; }
        }

        // Thursday property
        private string thu = "00:00-00:00";
        public string Thu
        {
            get { return thu; }
            set { thu = value; }
        }

        // Friday property
        private string fri = "00:00-00:00";
        public string Fri
        {
            get { return fri; }
            set { fri = value; }
        }

        // Saturday property
        private string sat = "00:00-00:00";
        public string Sat
        {
            get { return sat; }
            set { sat = value; }
        }

        // Sunday property
        private string sun = "00:00-00:00";
        public string Sun
        {
            get { return sun; }
            set { sun = value; }
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
