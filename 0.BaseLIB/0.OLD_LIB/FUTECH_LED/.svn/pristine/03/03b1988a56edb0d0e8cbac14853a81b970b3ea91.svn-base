using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class MapDetailCollection : CollectionBase
    {
        // Constructor
        public MapDetailCollection()
        {
 
        }

        public MapDetail this[int index]
        {
            get { return (MapDetail)InnerList[index]; }
        }

        // Add
        public void Add(MapDetail mapDetail)
        {
            InnerList.Add(mapDetail);
        }

        // Remove
        public void Remove(MapDetail mapDetail)
        {
            InnerList.Remove(mapDetail);
        }

        // Get MapDetail by it's mapID and controllerID
        public MapDetail GetMapDetail(int mapID, int controllerID)
        {
            foreach (MapDetail mapDetail in InnerList)
            {
                if (mapDetail.MapID == mapID && mapDetail.ControllerID == controllerID)
                {
                    return mapDetail;
                }
            }
            return null;
        }
    }
}
