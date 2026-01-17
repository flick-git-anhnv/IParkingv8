using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class GroupCollection : CollectionBase
    {
        // Constructor
        public GroupCollection()
        {
 
        }

        public Group this[int index]
        {
            get { return (Group)InnerList[index]; }
        }

        // Add
        public void Add(Group group)
        {
            InnerList.Add(group);
        }

        // Remove
        public void Remove(Group group)
        {
            InnerList.Remove(group);
        }

        // Get Group by it's ID
        public Group GetGroupByID(int id)
        {
            foreach (Group group in InnerList)
            {
                if (group.ID == id)
                {
                    return group;
                }
            }
            return null;
        }

        // Get Group by it's Code
        public Group GetGroupByCode(int departmentID, string code)
        {
            foreach (Group group in InnerList)
            {
                if (group.DepartmentID == departmentID && group.Code.ToString().ToUpper() == code.ToUpper())
                {
                    return group;
                }
            }
            return null;
        }

        // Get Group by it's name and departmentID
        public Group GetGroupByName(int departmentID, string name)
        {
            foreach (Group group in InnerList)
            {
                if (group.DepartmentID == departmentID && group.Name.ToUpper() == name.ToUpper())
                {
                    return group;
                }
            }
            return null;
        }

        // Get Group by it's name
        public Group GetGroupByName(string name)
        {
            foreach (Group group in InnerList)
            {
                if (group.Name.ToUpper() == name.ToUpper())
                {
                    return group;
                }
            }
            return null;
        }
    }
}
