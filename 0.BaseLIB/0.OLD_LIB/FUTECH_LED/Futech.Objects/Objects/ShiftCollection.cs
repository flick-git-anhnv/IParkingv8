using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class ShiftCollection : CollectionBase
    {
        // Constructor
        public ShiftCollection()
        {
 
        }

        public Shift this[int index]
        {
            get { return (Shift)InnerList[index]; }
        }

        // Add
        public void Add(Shift shift)
        {
            InnerList.Add(shift);
        }

        // Remove
        public void Remove(Shift shift)
        {
            InnerList.Remove(shift);
        }

        // Get Shift by it's ID
        public Shift GetShiftByID(int id)
        {
            foreach (Shift shift in InnerList)
            {
                if (shift.ID == id)
                {
                    return shift;
                }
            }
            return null;
        }

        // Get Shift by it's Code
        public Shift GetShiftByCode(string code)
        {
            foreach (Shift shift in InnerList)
            {
                if (shift.Code.ToUpper() == code.ToUpper())
                {
                    return shift;
                }
            }
            return null;
        }

        // Get Shift by it's Name
        public Shift GetShiftByName(string name)
        {
            foreach (Shift shift in InnerList)
            {
                if (shift.Name.ToUpper() == name.ToUpper())
                {
                    return shift;
                }
            }
            return null;
        }
    }
}
