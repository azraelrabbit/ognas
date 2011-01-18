using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Lib;
using Ognas.Lib.Cards;
using Platform.OgnasEventArgs;
using Ognas.Lib.Enums;
using Ognas.Lib.Skills;

namespace Platform.Games
{
    public class GameBase
    {
        /// <summary>
        /// 卡牌中心
        /// </summary>
        public CardCenter cardCenter
        {

            get;
            set;
        }

        /// <summary>
        /// 武将中心
        /// </summary>
        public ShogunCenter shogunCenter
        {
            get;
            set;
        }

        public List<Skill> CardList;

        public Dictionary<int, List<EnumUserRole>> seatList;

        /// <summary>
        /// 当前令牌用户
        /// </summary>
        User currentTokenUser
        {
            get;
            set;
        }

        /// <summary>
        /// 玩家列表
        /// </summary>
        public List<User> userList
        {
            get;
            set;
        }

        /// <summary>
        /// 当前游戏房间
        /// </summary>
        Room currentRoom
        {
            get;
            set;
        }

        public GameState gameState
        {
            get;
            set;
        }

        /// <summary>
        /// 事件规则处理中心
        /// </summary>
        EventCenterRule eventCenterRule;

        public event ShuffleCardComplete OnShuffleCardCompleted;
        public event SetUpSeatComplete OnSetUpSeatCompleted;
        public event SetUpUserRoleComplete OnSetUpUserRoleCompleted;
        public event DealCardBegin OnDealCardBegins;
        public event ShogunSelectionBegin OnShogunSelectionBegin;

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
            eventCenterRule = new EventCenterRule(this);
            //TODO: 

            // 初始化游戏全局变量
            gameState = GameState.GameStart;
            this.shogunCenter = new ShogunCenter();
            LoadCards();
            InitSeatList();

            // 洗牌
            ShuffleCard();

            // 随机设置座位次序
            SetUpSeats();

            // 分派游戏身份       
            SetUpUserRole();

            // 选择武将
            if (this.OnShogunSelectionBegin != null)
            {
                ShogunSelectArgs shogunArgs = new ShogunSelectArgs();
                shogunArgs.shogunCenter = this.shogunCenter;
                shogunArgs.Message = "开始选择武将...";
                gameState = GameState.SelectShogunBegin;
                shogunArgs.userList = this.userList;
                this.OnShogunSelectionBegin(this, shogunArgs);
            }

            // 发牌
            //  DealCards();

            // 用户令牌移交到第一顺位玩家

        }

        private void InitSeatList()
        {
            seatList = new Dictionary<int, List<EnumUserRole>>();
            //游戏人数	主公	    忠臣	 反贼	内奸
            //8	        1	    2	 4	    1
            List<EnumUserRole> seatSet = new List<EnumUserRole>();
            seatSet.Add(EnumUserRole.Lord);
            seatSet.Add(EnumUserRole.Loyal);
            seatSet.Add(EnumUserRole.Loyal);
            seatSet.Add(EnumUserRole.rebel);
            seatSet.Add(EnumUserRole.rebel);
            seatSet.Add(EnumUserRole.rebel);
            seatSet.Add(EnumUserRole.rebel);
            seatSet.Add(EnumUserRole.Traitor);

            seatList.Add(seatSet.Count, seatSet);
        }

        /// <summary>
        /// 读取所有的牌到牌堆中
        /// </summary>
        private void LoadCards()
        {
            // 读取所有的牌到牌堆中
            this.CardList = SkillUtility.GetSkillList();

        }

        /// <summary>
        /// 洗牌
        /// </summary>
        public void ShuffleCard()
        {
            // TODO: 洗牌
            this.CardList = Utility.GetRandomList(this.CardList, this.CardList.Count);

            // 洗牌完成 触发事件
            GameEventArgs gameArgs = new GameEventArgs();
            // gameArgs.sc = this.cardCenter;
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
            dcArgs.cardList = this.CardList;
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
            this.userList = Utility.GetRandomList(this.userList, this.userList.Count);
            int indexUser = 0;
            foreach (User u in userList)
            {
                u.SeatNum = indexUser.ToString();
                indexUser++;
            }
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
            List<EnumUserRole> roleList = this.seatList[8];
            roleList = Utility.GetRandomList(roleList, roleList.Count);
            for (int i = 0; i < userList.Count; i++)
            {
                this.userList[i].UserRole = roleList[i];
            }
            if (this.userList.Count == 1)
            {
                this.userList[0].UserRole = EnumUserRole.Lord;
            }
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
