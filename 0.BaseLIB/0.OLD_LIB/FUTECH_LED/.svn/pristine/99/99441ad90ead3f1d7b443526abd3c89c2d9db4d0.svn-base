using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class ControllerTypeCollection : CollectionBase
    {
        // Constructor
        public ControllerTypeCollection()
        {
 
        }

        public ControllerType this[int index]
        {
            get { return (ControllerType)InnerList[index]; }
        }

        // Add
        public void Add(ControllerType controllerType)
        {
            InnerList.Add(controllerType);
        }

        // Remove
        public void Remove(ControllerType controllerType)
        {
            InnerList.Remove(controllerType);
        }

        public ControllerType GetControllerTypeByID(int id)
        {
            foreach (ControllerType controllerType in InnerList)
            {
                if (controllerType.ID == id)
                {
                    return controllerType;
                }
            }
            return null;
        }

        public ControllerType GetControllerTypeByName(string name)
        {
            foreach (ControllerType controllerType in InnerList)
            {
                if (controllerType.Name.ToUpper() == name.ToUpper())
                {
                    return controllerType;
                }
            }
            return null;
        }
    }
}
