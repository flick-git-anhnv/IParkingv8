using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class LineTypeCollection : CollectionBase
    {
        // Constructor
        public LineTypeCollection()
        {
 
        }

        public LineType this[int index]
        {
            get { return (LineType)InnerList[index]; }
        }

        // Add
        public void Add(LineType lineType)
        {
            InnerList.Add(lineType);
        }

        // Remove
        public void Remove(LineType lineType)
        {
            InnerList.Remove(lineType);
        }

        public LineType GetLineTypeByID(int id)
        {
            foreach (LineType lineType in InnerList)
            {
                if (lineType.ID == id)
                {
                    return lineType;
                }
            }
            return null;
        }

        public LineType GetLineTypeByName(string name)
        {
            foreach (LineType lineType in InnerList)
            {
                if (lineType.Name == name)
                {
                    return lineType;
                }
            }
            return null;
        }
    }
}
