namespace Futech.Objects
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class PermissionCollection : CollectionBase
    {
        public void Add(Permission permission)
        {
            base.InnerList.Add(permission);
        }

        public Permission GetPermission(int userID, int fuctionID)
        {
            foreach (Permission permission in base.InnerList)
            {
                if ((permission.UserID == userID) && (permission.FunctionID == fuctionID))
                {
                    return permission;
                }
            }
            return null;
        }

        public void Remove(Permission permission)
        {
            base.InnerList.Remove(permission);
        }

        public Permission this[int index]
        {
            get
            {
                return (Permission) base.InnerList[index];
            }
        }
    }
}

