using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class BlackListCollection:CollectionBase
    {
        // Constructor
        public BlackListCollection()
        {
        }

        public BlackList this[int index]
        {
            get { return (BlackList)InnerList[index]; }
        }

        // Add
        public void Add(BlackList blacklist)
        {
            InnerList.Add(blacklist);
        }

        // Remove
        public void Remove(BlackList blacklist)
        {
            InnerList.Remove(blacklist);
        }

        // Get BlackList by it's ID
        public BlackList GetBlackListByID(int id)
        {
            foreach (BlackList blacklist in InnerList)
            {
                if (blacklist.ID == id)
                {
                    return blacklist;
                }
            }
            return null;
        }

        // Get BlackList by it's cardnumber
        public BlackList GetBlackListByCardNumber(string cardnumber)
        {
            foreach (BlackList blacklist in InnerList)
            {
                if (blacklist.CardNumber.ToUpper() == cardnumber.ToUpper())
                {
                    return blacklist;
                }
            }
            return null;
        }
    }
}
