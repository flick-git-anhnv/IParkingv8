using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class TitleCollection : CollectionBase
    {
        // Constructor
        public TitleCollection()
        {
 
        }

        public Title this[int index]
        {
            get { return (Title)InnerList[index]; }
        }

        // Add
        public void Add(Title title)
        {
            InnerList.Add(title);
        }

        // Remove
        public void Remove(Title title)
        {
            InnerList.Remove(title);
        }

        // Get Title by it's ID
        public Title GetTitleByID(int id)
        {
            foreach (Title title in InnerList)
            {
                if (title.ID == id)
                {
                    return title;
                }
            }
            return null;
        }

        // Get Title by it's code
        public Title GetTitleByCode(string code)
        {
            foreach (Title title in InnerList)
            {
                if (title.Code.ToUpper() == code.ToUpper())
                {
                    return title;
                }
            }
            return null;
        }

        // Get Title by it's name
        public Title GetTitleByName(string name)
        {
            foreach (Title title in InnerList)
            {
                if (title.Name.ToUpper() == name.ToUpper())
                {
                    return title;
                }
            }
            return null;
        }
    }
}
