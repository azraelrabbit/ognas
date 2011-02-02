using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

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
            this.UserState = EnumUserState.Live;
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
        }

        public List<Skills.Skill> HandCards
        {
            get;
            set;
        }
        public Shoguns.Shogun shogun
        {
            get;
            set;
        }

        public EnumUserState UserState
        {
            get;
            set;
        }

        public string Address { get; set; }
    }
}
