using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class VerifyTypeCollection : CollectionBase
    {
        // Constructor
        public VerifyTypeCollection()
        {
 
        }

        public VerifyType this[int index]
        {
            get { return (VerifyType)InnerList[index]; }
        }

        // Add
        public void Add(VerifyType verifyType)
        {
            InnerList.Add(verifyType);
        }

        // Remove
        public void Remove(VerifyType verifyType)
        {
            InnerList.Remove(verifyType);
        }

        public VerifyType GetVerifyTypeByID(int id)
        {
            foreach (VerifyType verifyType in InnerList)
            {
                if (verifyType.ID == id)
                {
                    return verifyType;
                }
            }
            return null;
        }

        public VerifyType GetVerifyTypeByName(string name)
        {
            foreach (VerifyType verifyType in InnerList)
            {
                if (verifyType.Name.ToUpper() == name.ToUpper())
                {
                    return verifyType;
                }
            }
            return null;
        }
    }
}
