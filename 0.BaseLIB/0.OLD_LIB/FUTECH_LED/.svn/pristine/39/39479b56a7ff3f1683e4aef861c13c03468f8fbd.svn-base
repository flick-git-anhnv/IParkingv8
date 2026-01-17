using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Futech.Objects
{
    public class UserCollection : CollectionBase
    {
        // Constructor
        public UserCollection()
        {
 
        }

        public User this[int index]
        {
            get { return (User)InnerList[index]; }
        }

        // Add
        public void Add(User user)
        {
            InnerList.Add(user);
        }

        // Remove
        public void Remove(User user)
        {
            InnerList.Remove(user);
        }

        // Get User by it's login
        public User GetUserByLogin(string login)
        {
            foreach (User user in InnerList)
            {
                if (user.Login == login)
                {
                    return user;
                }
            }
            return null;
        }

        // Get user by it's login and it's password
        public User GetUser(string login, string password)
        {
            foreach (User user in InnerList)
            {
                if (user.Login == login && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
