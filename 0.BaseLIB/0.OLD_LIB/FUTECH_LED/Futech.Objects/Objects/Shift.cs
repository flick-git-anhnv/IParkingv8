using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Shift
    {
        // Constructor
        public Shift()
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

        // Start Work
        private int startWork = 0;
        public int StartWork
        {
            get { return startWork; }
            set { startWork = value; }
        }

        // Stop Work
        private int stopWork = 0;
        public int StopWork
        {
            get { return stopWork; }
            set { stopWork = value; }
        }

        // Start Break
        private int startBreak = 0;
        public int StartBreak
        {
            get { return startBreak; }
            set { startBreak = value; }
        }

        // Stop Break
        private int stopBreak = 0;
        public int StopBreak
        {
            get { return stopBreak; }
            set { stopBreak = value; }
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
