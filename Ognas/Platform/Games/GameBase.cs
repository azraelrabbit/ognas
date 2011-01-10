using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;

namespace Platform.Games
{
    public class GameBase
    {
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

            // 洗牌

            // 随机设置座位次序

            // 分派游戏身份            

            // 选择武将

            // 发牌

            // 用户令牌移交到第一顺位玩家
        }
    }
}
