using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Futech.Objects
{
    public class LineCollection : CollectionBase
    {
        // Constructor
        public LineCollection()
        {
 
        }

        public Line this[int index]
        {
            get { return (Line)InnerList[index]; }
        }

        // Add
        public void Add(Line line)
        {
            InnerList.Add(line);
        }

        // Remove
        public void Remove(Line line)
        {
            InnerList.Remove(line);
        }

        // Get Line by it's ID
        public Line GetLineByID(int id)
        {
            foreach (Line line in InnerList)
            {
                if (line.ID == id)
                {
                    return line;
                }
            }
            return null;
        }

        // Get Line by it's Code
        public Line GetLineByCode(string code)
        {
            foreach (Line line in InnerList)
            {
                if (line.Code == code)
                {
                    return line;
                }
            }
            return null;
        }

        // Get Line by it's Name
        public Line GetLineByName(string name)
        {
            foreach (Line line in InnerList)
            {
                if (line.Name == name)
                {
                    return line;
                }
            }
            return null;
        }

        // Get Line by it's COM Port or IP address
        public Line GetLineByComPort(string comPort)
        {
            foreach (Line line in InnerList)
            {
                if (line.ComPort == comPort)
                {
                    return line;
                }
            }
            return null;
        }
    }
}
