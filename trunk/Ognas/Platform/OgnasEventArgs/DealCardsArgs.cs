using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;

namespace Platform.OgnasEventArgs
{
    public class DealCardsArgs : EventArgs
    {
        public List<Card> cardList
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
