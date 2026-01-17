using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class HolidayCollection : CollectionBase
    {
        // Constructor
        public HolidayCollection()
        {

        }

        public Holiday this[int index]
        {
            get { return (Holiday)InnerList[index]; }
        }

        // Add
        public void Add(Holiday holiday)
        {
            InnerList.Add(holiday);
        }

        // Remove
        public void Remove(Holiday holiday)
        {
            InnerList.Remove(holiday);
        }

        // Get holiday by ID
        public Holiday GetHolidayByID(int id)
        {
            foreach (Holiday holiday in InnerList)
            {
                if (holiday.ID == id)
                {
                    return holiday;
                }
            }
            return null;
        }

        // Get holiday by Code
        public Holiday GetHolidayByCode(string code)
        {
            foreach (Holiday holiday in InnerList)
            {
                if (holiday.Code.ToUpper() == code.ToUpper())
                {
                    return holiday;
                }
            }
            return null;
        }

        // Get holiday by Name
        public Holiday GetHolidayByName(string name)
        {
            foreach (Holiday holiday in InnerList)
            {
                if (holiday.Name.ToUpper() == name.ToUpper())
                {
                    return holiday;
                }
            }
            return null;
        }
    }
}
