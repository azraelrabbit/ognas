using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Model
{
    public class User
    {
        public User(string userName)
        {
            this.userName = userName;
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
        }
        

        public string Address { get; set; }

        

        //public Role Role { get; set; }

        //public Generic General { get; set; }

        public Room Room { get; set; }
    }
}
