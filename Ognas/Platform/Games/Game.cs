using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Ognas.Server.Cards;

namespace Platform.Games
{
    public class Game : GameBase
    {
        public Game()
        {
        }

        public Game(List<User> userlist)
        {
            this.userList = userlist;
        }
    }
}
