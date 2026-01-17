using System;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class Door
    {
        //CTO
        public Door()
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

        //ControllerCode
        private string controllerCode = "";
        public string ControllerCode
        {
            get { return controllerCode; }
            set { controllerCode = value; }
        }

        //reader index
        private int readerindex = 0;
        public int ReaderIndex
        {
            get { return readerindex; }
            set { readerindex = value; }
        }

    }
}
