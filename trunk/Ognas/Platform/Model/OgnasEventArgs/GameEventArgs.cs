using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;

namespace Platform.Model.OgnasEventArgs
{
    public class GameEventArgs : EventArgs
    {
        public List<Card> cardList
        {
            get;
            set;
        }

        public List<User> userList
        {
            get;
            set;
        }

        public string Messages
        {
            get;
            set;
        }

    }
}
