using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class DepartmentCollection : CollectionBase
    {
        // Constructor
        public DepartmentCollection()
        {
 
        }

        public Department this[int index]
        {
            get { return (Department)InnerList[index]; }
        }

        // Add
        public void Add(Department department)
        {
            InnerList.Add(department);
        }

        // Remove
        public void Remove(Department department)
        {
            InnerList.Remove(department);
        }

        // Get Department by it's ID
        public Department GetDepartmentByID(int id)
        {
            foreach (Department department in InnerList)
            {
                if (department.ID == id)
                {
                    return department;
                }
            }
            return null;
        }

        // Get Department by it's code
        public Department GetDepartmentByCode(string code)
        {
            foreach (Department department in InnerList)
            {
                if (department.Code.ToUpper() == code.ToUpper())
                {
                    return department;
                }
            }
            return null;
        }

        // Get Department by it's name
        public Department GetDepartmentByName(string name)
        {
            foreach (Department department in InnerList)
            {
                if (department.Name.ToUpper() == name.ToUpper())
                {
                    return department;
                }
            }
            return null;
        }
    }
}
