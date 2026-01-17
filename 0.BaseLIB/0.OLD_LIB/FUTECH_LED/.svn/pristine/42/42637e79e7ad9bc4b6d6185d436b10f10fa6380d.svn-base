using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class HolidayTypeCollection : CollectionBase
    {
        // Constructor
        public HolidayTypeCollection()
        {

        }

        // Add
        public void Add(HolidayType holidayType)
        {
            InnerList.Add(holidayType);
        }

        // Remove
        public void Remove(HolidayType holidayType)
        {
            InnerList.Remove(holidayType);
        }

        // Get holidayType by ID
        public HolidayType GetHolidayTypeByID(int id)
        {
            foreach (HolidayType holidayType in InnerList)
            {
                if (holidayType.ID == id)
                {
                    return holidayType;
                }
            }
            return null;
        }

        // Get holidayType by Code
        public HolidayType GetHolidayTypeByCode(string code)
        {
            foreach (HolidayType holidayType in InnerList)
            {
                if (holidayType.Code.ToUpper() == code.ToUpper())
                {
                    return holidayType;
                }
            }
            return null;
        }

        // Get holidayType by Name
        public HolidayType GetHolidayTypeByName(string name)
        {
            foreach (HolidayType holidayType in InnerList)
            {
                if (holidayType.Name.ToUpper() == name.ToUpper())
                {
                    return holidayType;
                }
            }
            return null;
        }
    }
}
