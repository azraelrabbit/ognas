using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Cards;

namespace Ognas.Lib
{
    public class CardCenter
    {
        public SortedList<int, Card> _slPoker = new SortedList<int, Card>();

        public CardCenter()
        { }

        /// <summary>
        /// 发牌
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="count">指定张数</param>
        public void Deal(object target, int count)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 洗牌
        /// </summary>
        public void Shuffle()
        {
            throw new NotImplementedException();
        }
    }
}
