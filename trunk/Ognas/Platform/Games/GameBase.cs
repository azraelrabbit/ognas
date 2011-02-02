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

        // 当前玩家令牌次序
        public int currentToken
        {
            get;
            set;
        }

        // 回合状态
        public EnumAroundState GoAroundState
        {
            get;
            set;
        }

        /// <summary>
        /// 回合数
        /// </summary>
        public int GoAround
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

        // 分派座位 角色
        public event SetUpSeatComplete OnSetUpSeatCompleted;
        public event SetUpUserRoleComplete OnSetUpUserRoleCompleted;

        // 发牌
        public event DealCardBegin OnDealCardBegins;
        public event DealCardComplete OnDealCardCompleted;

        // 选择武将
        public event ShogunSelectionBegin OnShogunSelectionBegin;
        public event ShogunSelectionLordComplete OnShogunSelectionLordCompleted;
        public event ShogunSelectionOtherBegin OnShogunSelectionOtherBegin;
        public event ShogunSelectionOtherComplete OnShogunSelectionOtherCompleted;


        // 出牌 摸牌 弃牌
        public event UserCardUse OnUserCardUsing;
        public event UserCardsGet OnUserCardGetting;
        public event UserCardsDrop OnUserCardDroping;

        public GameBase()
        {
            this.userList = new List<User>();
            this.sortedUsers = new SortedList<int, User>();
        }

        public GameBase(List<User> userlist)
        {
            this.userList = userlist;
            this.sortedUsers = new SortedList<int, User>();
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
            dcArgs.userList = this.userList;

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
            foreach (User u in this.userList)
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


            // 生成出牌次序列表
            int tmp = 0;
            foreach (User u in this.userList)
            {
                if (u.UserRole == EnumUserRole.Lord)
                {
                    tmp = int.Parse(u.SeatNum);
                }
            }
            int intNm = 0;
            for (int i = tmp; i >= 0; i--)
            {
                this.sortedUsers.Add(intNm, this.userList[i]);
                intNm++;
            }
            for (int i = this.userList.Count - 1; i > tmp; i--)
            {
                this.sortedUsers.Add(intNm, this.userList[i]);
                intNm++;
            }
            // 设置第一个玩家也就是主公为当前玩家
            this.currentToken = 0;

        }
        public SortedList<int, User> sortedUsers
        {
            get;
            set;
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

        public void SelectedShogunLoardCompleted(Room room, ShogunSelectArgs ssa)
        {
            shogunCenter.Shoguns.Remove(ssa.SelectedShogun);
            if (this.OnShogunSelectionLordCompleted != null)
            {
                this.OnShogunSelectionLordCompleted(room, ssa);
            }
        }
        public void SelectedShogunOtherBegin()
        {
            ShogunSelectArgs ssa = new ShogunSelectArgs();
            ssa.shogunCenter = this.shogunCenter;
            ssa.userList = this.userList;
            if (this.OnShogunSelectionOtherBegin != null)
            {
                this.OnShogunSelectionOtherBegin(this, ssa);
            }
        }

        /// <summary>
        /// 获取下一令牌用户
        /// </summary>
        /// <returns></returns>
        public User NextUser()
        {
            this.currentTokenUser = this.sortedUsers[this.currentToken];
            if (this.currentToken == 0)
            {
                this.GoAroundState = EnumAroundState.AroundStart;
            }

            if (this.currentToken == this.sortedUsers.Count)
            {
                this.GoAroundState = EnumAroundState.AroundOver;
                this.currentToken = 0;
            }
            else
            {
                this.currentToken++;
            }

            return this.currentTokenUser;
        }
    }
}
