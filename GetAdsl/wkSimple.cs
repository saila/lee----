using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetAdsl
{
    class wkSimple
    {
        public string user;
        public string password;
        public wkSimple(string user, string password) 
        {
            this.user = user;
            this.password = password;
        }
        public string Getuser()
        {
            return this.user;
        }
        public string Getpassword() 
        {
            return this.password;
        }
        
    }
}
