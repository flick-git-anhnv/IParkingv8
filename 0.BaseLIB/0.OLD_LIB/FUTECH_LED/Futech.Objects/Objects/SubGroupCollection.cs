using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class SubGroupCollection : CollectionBase
    {
        // Constructor
        public SubGroupCollection()
        {

        }

        public SubGroup this[int index]
        {
            get { return (SubGroup)InnerList[index]; }
        }

        // Add
        public void Add(SubGroup subgroup)
        {
            InnerList.Add(subgroup);
        }

        // Remove
        public void Remove(SubGroup subgroup)
        {
            InnerList.Remove(subgroup);
        }

        // Get SubGroup by it's ID
        public SubGroup GetSubGroupByID(int id)
        {
            foreach (SubGroup subgroup in InnerList)
            {
                if (subgroup.ID == id)
                {
                    return subgroup;
                }
            }
            return null;
        }

        // Get SubGroup by it's Code and groupID
        public SubGroup GetSubGroupByCode(int groupID, string code)
        {
            foreach (SubGroup subgroup in InnerList)
            {
                if (subgroup.GroupID == groupID && subgroup.Code.ToString().ToUpper() == code.ToUpper())
                {
                    return subgroup;
                }
            }
            return null;
        }

        // Get SubGroup by it's name and groupID
        public SubGroup GetSubGroupByName(int groupID, string name)
        {
            foreach (SubGroup subgroup in InnerList)
            {
                if (subgroup.GroupID == groupID && subgroup.Name.ToUpper() == name.ToUpper())
                {
                    return subgroup;
                }
            }
            return null;
        }

        // Get SubGroup by it's name
        public SubGroup GetSubGroupByName(string name)
        {
            foreach (SubGroup subgroup in InnerList)
            {
                if (subgroup.Name.ToUpper() == name.ToUpper())
                {
                    return subgroup;
                }
            }
            return null;
        }

    }
}
