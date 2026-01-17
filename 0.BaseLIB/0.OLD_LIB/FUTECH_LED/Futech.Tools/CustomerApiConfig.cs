using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futech.Tools
{
    public class CustomerApiConfig
    {
            private string _Url;
            private string _UserName;
            private string _Password;

            public string Url { get => _Url; set => _Url = value; } 
            public string UserName { get => _UserName; set => _UserName = value; }
            public string Password { get => _Password; set => _Password = value; }
    }
}
