using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Games
{
    public class EventCenterRule
    {
        public EventCenterRule(Game game)
        {
            game.OnShuffleCardCompleted += new Model.ShuffleCardComplete(game_OnShuffleCardCompleted);
            game.OnSetUpSeatCompleted += new Model.SetUpSeatComplete(game_OnSetUpSeatCompleted);
            game.OnSetUpUserRoleCompleted += new Model.SetUpUserRoleComplete(game_OnSetUpUserRoleCompleted);
            game.OnDealCardBegins += new Model.DealCardBegin(game_OnDealCardBegins);
        }

        /// <summary>
        /// 开始发牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnDealCardBegins(object sender, OgnasEventArgs.DealCardsArgs args)
        {
            // TODO: 开始发牌
        }

        /// <summary>
        /// 身份分派结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnSetUpUserRoleCompleted(object sender, OgnasEventArgs.GameEventArgs args)
        {
            // TODO: 通知客户端身份分派结果
        }

        /// <summary>
        /// 座位分派结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnSetUpSeatCompleted(object sender, OgnasEventArgs.GameEventArgs args)
        {
            // TODO: 通知客户端座位结果

        }

        /// <summary>
        /// 洗牌结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnShuffleCardCompleted(object sender, OgnasEventArgs.GameEventArgs args)
        {
            Console.WriteLine(args.Messages);
        }
    }
}
