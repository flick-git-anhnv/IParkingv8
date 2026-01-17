using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class AccessLevelDetailCollection : CollectionBase
    {
        // Constructor
        public AccessLevelDetailCollection()
        {
 
        }

        public AccessLevelDetail this[int index]
        {
            get { return (AccessLevelDetail)InnerList[index]; }
        }

        // Add
        public void Add(AccessLevelDetail accessLevelDetail)
        {
            InnerList.Add(accessLevelDetail);
        }

        // Remove
        public void Remove(AccessLevelDetail accessLevelDetail)
        {
            InnerList.Remove(accessLevelDetail);
        }

        // Get AccessLevelDetail
        public AccessLevelDetail GetAccessLevelDetail(int accessLevelID, int controllerID, int timezoneID)
        {
            foreach (AccessLevelDetail accessLevelDetail in InnerList)
            {
                if (accessLevelDetail.AccessLevelID == accessLevelID && accessLevelDetail.ControllerID == controllerID && accessLevelDetail.TimezoneID == timezoneID)
                {
                    return accessLevelDetail;
                }
            }
            return null;
        }
    }
}
