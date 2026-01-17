using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Futech.Objects
{
    public class DoorCollection:CollectionBase
    {
        //CTO
        public DoorCollection()
        { }

        public Door this[int index]
        {
            get { return (Door)InnerList[index]; }
        }

        // Add
        public void Add(Door door)
        {
            InnerList.Add(door);
        }
    
        // Remove
        public void Remove(Door door)
        {
            InnerList.Remove(door);
        }

        // Get Door by it's ID
        public Door GetDoorByID(int id)
        {
            foreach (Door door in InnerList)
            {
                if (door.ID == id)
                {
                    return door;
                }
            }
            return null;
        }

        // Get Door by it's Code
        public Door GetDoorByCode(string code)
        {
            foreach (Door door in InnerList)
            {
                if (door.Code == code)
                {
                    return door;
                }
            }
            return null;
        }

        // Get Door by it's Name
        public Door GetDoorByName(string name)
        {
            foreach (Door door in InnerList)
            {
                if (door.Name == name)
                {
                    return door;
                }
            }
            return null;
        }

        public Door GetDoorByControllerCodeAndReaderIndex(string controllerCode,int readerindex)
        {
            foreach (Door door in InnerList)
            {
                if (door.ControllerCode==controllerCode&&door.ReaderIndex==readerindex)
                {
                    return door;
                }
            }
            return null;
        }
    }
}
