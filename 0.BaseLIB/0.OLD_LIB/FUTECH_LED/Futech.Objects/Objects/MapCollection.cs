using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class MapCollection : CollectionBase
    {
        // Constructor
        public MapCollection()
        {
 
        }

        public Map this[int index]
        {
            get { return (Map)InnerList[index]; }
        }

        // Add
        public void Add(Map map)
        {
            InnerList.Add(map);
        }

        // Remove
        public void Remove(Map map)
        {
            InnerList.Remove(map);
        }

        // Get Map by it's ID
        public Map GetMapByID(int id)
        {
            foreach (Map map in InnerList)
            {
                if (map.ID == id)
                {
                    return map;
                }
            }
            return null;
        }

        // Get Map by it's Name
        public Map GetMapByName(string name)
        {
            foreach (Map map in InnerList)
            {
                if (map.Name == name)
                {
                    return map;
                }
            }
            return null;
        }
    }
}
