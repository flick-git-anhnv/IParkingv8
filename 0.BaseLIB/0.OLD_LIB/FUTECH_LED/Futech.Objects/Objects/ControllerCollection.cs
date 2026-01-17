using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Futech.Objects
{
    public class ControllerCollection : CollectionBase
    {
        // Constructor
        public ControllerCollection()
        {
 
        }

        public Controller this[int index]
        {
            get { return (Controller)InnerList[index]; }
        }

        // Add
        public void Add(Controller controller)
        {
            InnerList.Add(controller);
        }

        // Remove
        public void Remove(Controller controller)
        {
            InnerList.Remove(controller);
        }

        // Get Controller by it's ID
        public Controller GetControllerByID(int id)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.ID == id)
                {
                    return controller;
                }
            }
            return null;
        }

        // Get Controller by it's Code
        public Controller GetControllerByCode(string code)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.Code == code)
                {
                    return controller;
                }
            }
            return null;
        }

        // Get Controller by it's Name
        public Controller GetControllerByName(string name)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.Name == name)
                {
                    return controller;
                }
            }
            return null;
        }

        // Get Controller by it's Address
        public Controller GetControllerByAddress(int address)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.Address == address)
                {
                    return controller;
                }
            }
            return null;
        }

        // Get Controller by it's Address and LineID
        public Controller GetControllerByAddress(int lineID, int address)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.LineID == lineID && controller.Address == address)
                {
                    return controller;
                }
            }
            return null;
        }

        // Get Controller by it's Address and LineCode
        public Controller GetControllerByAddressAndCode(string lineCode, int address)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.LineCode == lineCode && controller.Address == address)
                {
                    return controller;
                }
            }
            return null;
        }

        public Controller GetControllerByLineCodeAndAddress(string linecode, int address)
        {
            foreach (Controller controller in InnerList)
            {
                if (controller.LineCode == linecode && controller.Address == address)
                {
                    return controller;
                }
            }
            return null;
        }
    }
}
