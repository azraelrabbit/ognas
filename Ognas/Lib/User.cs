using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enum;

namespace Ognas.Lib
{
    public class User
    {
        public string SeatNum
        {
            get;
            set;
        }
        public EnumUserRole UserRole
        {
            get;
            set;
        }
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
    }
}
