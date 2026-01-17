using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Futech.Objects
{
    public class SpecialHolidayCollection : CollectionBase
    {
        // Constructor
        public SpecialHolidayCollection()
        {
 
        }

        // Add
        public void Add(SpecialHoliday specialHoliday)
        {
            InnerList.Add(specialHoliday);
        }

        // Remove
        public void Remove(SpecialHoliday specialHoliday)
        {
            InnerList.Remove(specialHoliday);
        }

        // Get specialHoliday
        public SpecialHoliday GetSpecialHolidayWithYear(int day, int month, int year)
        {
            foreach (SpecialHoliday specialHoliday in InnerList)
            {
                if (specialHoliday.Day == day && specialHoliday.Month == month && specialHoliday.Year == year)
                {
                    return specialHoliday;
                }
            }
            return null;
        }

        // Get specialHoliday
        public SpecialHoliday GetSpecialHoliday(int day, int month)
        {
            foreach (SpecialHoliday specialHoliday in InnerList)
            {
                if (specialHoliday.Day == day && specialHoliday.Month == month && specialHoliday.IsEveryYear == true)
                {
                    return specialHoliday;
                }
            }
            return null;
        }
    }
}
