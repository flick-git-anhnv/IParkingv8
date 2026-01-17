namespace Futech.Objects
{
    using System;

    public class Permission
    {
        private string deletes = "";
        private int functionID = 0;
        private string inserts = "";
        private string selects = "";
        private string updates = "";
        private string prints = "";
        private string exports = "";
        private int userID = 0;

        public string Deletes
        {
            get
            {
                return this.deletes;
            }
            set
            {
                this.deletes = value;
            }
        }

        public int FunctionID
        {
            get
            {
                return this.functionID;
            }
            set
            {
                this.functionID = value;
            }
        }

        public string Inserts
        {
            get
            {
                return this.inserts;
            }
            set
            {
                this.inserts = value;
            }
        }

        public string Selects
        {
            get
            {
                return this.selects;
            }
            set
            {
                this.selects = value;
            }
        }

        public string Updates
        {
            get
            {
                return this.updates;
            }
            set
            {
                this.updates = value;
            }
        }

        public int UserID
        {
            get
            {
                return this.userID;
            }
            set
            {
                this.userID = value;
            }
        }

        public string Prints
        {
            get { return this.prints; }
            set { this.prints = value; }
        }

        public string Exports
        {
            get { return this.exports; }
            set { this.exports = value; }
        }
    }
}

