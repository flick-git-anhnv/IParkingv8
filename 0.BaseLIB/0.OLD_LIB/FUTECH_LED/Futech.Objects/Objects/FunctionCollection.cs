namespace Futech.Objects
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class FunctionCollection : CollectionBase
    {
        public void Add(Function function)
        {
            base.InnerList.Add(function);
        }

        public Function GetFunctionByID(int id)
        {
            foreach (Function function in base.InnerList)
            {
                if (function.ID == id)
                {
                    return function;
                }
            }
            return null;
        }

        public Function GetFunctionByName(string name)
        {
            foreach (Function function in base.InnerList)
            {
                if (function.Name == name)
                {
                    return function;
                }
            }
            return null;
        }

        public void Remove(Function function)
        {
            base.InnerList.Remove(function);
        }

        public Function this[int index]
        {
            get
            {
                return (Function) base.InnerList[index];
            }
        }
    }
}

