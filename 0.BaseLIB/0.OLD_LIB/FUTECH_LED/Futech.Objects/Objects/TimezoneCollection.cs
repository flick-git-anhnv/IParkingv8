using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class TimezoneCollection : CollectionBase
    {
        // Constructor
        public TimezoneCollection()
        {
 
        }
        public Timezone this[int index]
        {
            get { return (Timezone)InnerList[index]; }
        }


        // Add
        public void Add(Timezone timezone)
        {
            InnerList.Add(timezone);
        }

        // Remove
        public void Remove(Timezone timezone)
        {
            InnerList.Remove(timezone);
        }

        // Get Timezone by ID
        public Timezone GetTimezoneByID(int id)
        {
            foreach (Timezone timezone in InnerList)
            {
                if (timezone.ID == id)
                    return timezone;
            }
            return null;
        }

        // Get Timezone by Name
        public Timezone GetTimezoneByName(string name)
        {
            foreach (Timezone timezone in InnerList)
            {
                if (timezone.Name == name)
                    return timezone;
            }
            return null;
        }
    }
}
