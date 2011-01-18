using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib;

namespace Platform.OgnasEventArgs
{
    public class ShogunSelectArgs : EventArgs
    {
        public ShogunCenter shogunCenter
        {
            get;
            set;
        }

        public List<User> userList
        {
            get;
            set;
        }

        public Ognas.Lib.Protocols.SelectionShogunProtocol ssProtocol
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}
