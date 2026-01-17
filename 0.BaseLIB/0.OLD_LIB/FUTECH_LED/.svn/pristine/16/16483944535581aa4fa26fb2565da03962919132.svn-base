using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class DeviceCollection:CollectionBase
    {
        // Constructor
        public DeviceCollection()
        {
        }

        public Device this[int index]
        {
            get { return (Device)InnerList[index]; }
        }

        // Add
        public void Add(Device device)
        {
            InnerList.Add(device);
        }

        // Remove
        public void Remove(Device device)
        {
            InnerList.Remove(device);
        }

        // Get Device by it's ID
        public Device GetDeviceByID(int id)
        {
            foreach (Device device in InnerList)
            {
                if (device.ID == id)
                {
                    return device;
                }
            }
            return null;
        }

        // Get Device by it's name
        public Device GetDeviceByName(string name)
        {
            foreach (Device device in InnerList)
            {
                if (device.Name.ToUpper() == name.ToUpper())
                {
                    return device;
                }
            }
            return null;
        }

        // Get Device by it's code
        public Device GetDeviceByCode(string code)
        {
            foreach (Device device in InnerList)
            {
                if (device.Code.ToUpper() == code.ToUpper())
                {
                    return device;
                }
            }
            return null;
        }
    }
}
