using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;
using Platform.Model.OgnasEventArgs;

namespace Platform.Games
{
    public class GameBase
    {
        /// <summary>
        /// 底牌堆
        /// </summary>
        public List<Card> cardsList
        {
            get;
            set;
        }

        /// <summary>
        /// 弃牌堆
        /// </summary>
        public List<Card> dropCardsList
        {
            get;
            set;
        }

        User currentTokenUser
        {
            get;
            set;
        }

        public List<User> userList
        {
            get;
            set;
        }

        Room currentRoom
        {
            get;
            set;
        }

        public GameBase()
        {
        }

        public GameBase(List<User> userlist)
        {
            this.userList = userlist;
        }

        /// <summary>
        /// Game Starting
        /// </summary>
        public void GameStart()
        {
            //TODO: 

            // 初始化游戏全局变量
            LoadCards();

            // 洗牌
            ShuffleCard();

            // 随机设置座位次序
            SetUpSeats();

            // 分派游戏身份       
            SetUpUserRole();

            // 选择武将

            // 发牌
            DealCards();

            // 用户令牌移交到第一顺位玩家

        }


        /// <summary>
        /// 读取所有的牌到牌堆中
        /// </summary>
        private void LoadCards()
        {
            // 读取所有的牌到牌堆中
        }

        public event ShuffleCardComplete OnShuffleCardCompleted;
        public event SetUpSeatComplete OnSetUpSeatCompleted;
        public event SetUpUserRoleComplete OnSetUpUserRoleCompleted;
        public event DealCardBegin OnDealCardBegins;

        /// <summary>
        /// 洗牌
        /// </summary>
        public void ShuffleCard()
        {
            // TODO: 洗牌

            // 洗牌完成 触发事件
            GameEventArgs gameArgs = new GameEventArgs();

            gameArgs.Messages = "洗牌完成...";

            if (OnShuffleCardCompleted != null)
            {
                this.OnShuffleCardCompleted(this, gameArgs);
            }
        }

        /// <summary>
        /// 发牌
        /// </summary>
        public void DealCards()
        {
            // TODO: 发牌
            // 发牌
            DealCardsArgs dcArgs = new DealCardsArgs();
            dcArgs.cardList = this.cardsList;
            dcArgs.Messages = "开始发牌...";
            if (this.OnDealCardBegins != null)
            {
                this.OnDealCardBegins(this, dcArgs);
            }
        }

        /// <summary>
        /// 排座
        /// </summary>
        public void SetUpSeats()
        {
            // TODO: 随机排座

            // 座位分派完毕
            GameEventArgs gameArgs = new GameEventArgs();
            gameArgs.userList = this.userList;
            gameArgs.Messages = "座位分派完毕...";
            if (this.OnSetUpSeatCompleted != null)
            {
                this.OnSetUpSeatCompleted(this, gameArgs);
            }
        }

        /// <summary>
        /// 分派身份
        /// </summary>
        public void SetUpUserRole()
        {
            // TODO: 分派身份

            // 分派身份完毕
            GameEventArgs gameArgs = new GameEventArgs();
            gameArgs.userList = this.userList;
            gameArgs.Messages = "身份分派完毕...";
            if (this.OnSetUpUserRoleCompleted != null)
            {
                this.OnSetUpUserRoleCompleted(this, gameArgs);
            }

        }

    }
}
