using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib.Cards;
using Ognas.Lib;
using Ognas.Lib.Skills;
namespace Platform.OgnasEventArgs
{
    public class DealCardsArgs : EventArgs
    {
        public List<Skill> cardList
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
