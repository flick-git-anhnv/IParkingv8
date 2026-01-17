using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class AccessLevelCollection : CollectionBase
    {
        // Constructor
        public AccessLevelCollection()
        {
 
        }

        public AccessLevel this[int index]
        {
            get { return (AccessLevel)InnerList[index]; }
        }

        // Add
        public void Add(AccessLevel accesslevel)
        {
            InnerList.Add(accesslevel);
        }

        // Remove
        public void Remove(AccessLevel accesslevel)
        {
            InnerList.Remove(accesslevel);
        }

        // Get AccessLevel
        public AccessLevel GetAccessLevelByID(int id)
        {
            foreach (AccessLevel accesslevel in InnerList)
            {
                if (accesslevel.ID == id)
                {
                    return accesslevel;
                }
            }
            return null;
        }

        // Get AccessLevel
        public AccessLevel GetAccessLevelByName(string name)
        {
            foreach (AccessLevel accesslevel in InnerList)
            {
                if (accesslevel.Name == name)
                {
                    return accesslevel;
                }
            }
            return null;
        }
    }
}
