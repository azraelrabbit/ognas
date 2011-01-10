using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;

namespace Platform.OgnasEventArgs
{
    public class CardUseEventArgs : EventArgs
    {
        public User sourceUser
        {
            get;
            set;
        }

        public User targetUser
        {
            get;
            set;
        }

        public Card card
        {
            get;
            set;
        }

    }
}
