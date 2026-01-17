using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class EmployeeCollection : CollectionBase
    {
        // Constructor
        public EmployeeCollection()
        {
 
        }

        public Employee this[int index]
        {
            get
            {
                if (InnerList.Count > 0)
                    return (Employee)InnerList[index];
                else
                    return null;
            }
        }

        // Add
        public void Add(Employee employee)
        {
            InnerList.Add(employee);
        }

        // Remove
        public void Remove(Employee employee)
        {
            InnerList.Remove(employee);
        }

        // Get Employee by it's ID
        public Employee GetEmployeeByID(int id)
        {
            foreach (Employee employee in InnerList)
            {
                if (employee.ID == id)
                {
                    return employee;
                }
            }
            return null;
        }

        // Get Employee by it's Code
        public Employee GetEmployeeByCode(string code)
        {
            foreach (Employee employee in InnerList)
            {
                if (employee.Code.ToUpper() == code.ToUpper())
                {
                    return employee;
                }
            }
            return null;
        }

        // Get Employee by it's CardNumber
        public Employee GetEmployeeByCardNumber(string cardnumber)
        {
            foreach (Employee employee in InnerList)
            {
                if (employee.CardNumber.ToUpper() == cardnumber.ToUpper())
                {
                    return employee;
                }
            }
            return null;
        }

        // Get Employee
        public Employee GetEmployee(string codeAndName)
        {
            foreach (Employee employee in InnerList)
            {
                if (employee.Code + " - " + employee.Name == codeAndName)
                {
                    return employee;
                }
            }
            return null;
        }

        // Get Employee by Name
        public Employee GetEmployeeByName(string name)
        {
            foreach (Employee employee in InnerList)
            {
                if (employee.Name == name)
                {
                    return employee;
                }
            }
            return null;
        }

    }
}
