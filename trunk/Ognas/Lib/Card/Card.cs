
namespace Ognas.Server.Card
{
    public class Card
    {
        /// <summary>
        /// 卡牌名称（杀，闪。。。。）
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 卡牌代码，唯一标示一张卡牌(01,02,1A,1B.....)
        /// </summary>
        public string Code
        {
            get;
            set;
        }

        /// <summary>
        /// 卡牌花色（红色，黑色）
        /// </summary>
        public CardColor Color
        {
            get;
            set;
        }

        /// <summary>
        /// 卡牌点数（A,2,3,4,5,6,7,8,9,10,J,Q,K）
        /// </summary>
        public CardNumber Number
        {
            get;
            set;
        }

        /// <summary>
        /// 卡牌说明
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
    }
}
