using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class DeviceCategoryCollection:CollectionBase
    {
         // Constructor
        public DeviceCategoryCollection()
        {
 
        }

        public DeviceCategory this[int index]
        {
            get { return (DeviceCategory)InnerList[index]; }
        }

        // Add
        public void Add(DeviceCategory devicecategory)
        {
            InnerList.Add(devicecategory);
        }

        // Remove
        public void Remove(DeviceCategory devicecategory)
        {
            InnerList.Remove(devicecategory);
        }

        // Get DeviceCategory by it's ID
        public DeviceCategory GetDeviceCategoryByID(int id)
        {
            foreach (DeviceCategory devicecategory in InnerList)
            {
                if (devicecategory.ID == id)
                {
                    return devicecategory;
                }
            }
            return null;
        }

        // Get DeviceCategory by it's name
        public DeviceCategory GetDeviceCategoryByName(string name)
        {
            foreach (DeviceCategory devicecategory in InnerList)
            {
                if (devicecategory.Name.ToUpper() == name.ToUpper())
                {
                    return devicecategory;
                }
            }
            return null;
        }
    }
}
