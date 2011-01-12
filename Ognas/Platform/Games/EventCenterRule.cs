using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using Platform.Protocols;
using System.Collections;
using LibProtocols = Ognas.Lib.Protocols;
using Ognas.Lib.Protocols;
using Ognas.Lib;
using Ognas.Lib.Enum;
using Ognas.Lib.SocketUtils;

namespace Platform.Games
{
    public class EventCenterRule
    {
        Room room;

        public EventCenterRule(object objgame)
        {
            Game game = (Game)objgame;
            game.OnShuffleCardCompleted += new Model.ShuffleCardComplete(game_OnShuffleCardCompleted);
            game.OnSetUpSeatCompleted += new Model.SetUpSeatComplete(game_OnSetUpSeatCompleted);
            game.OnSetUpUserRoleCompleted += new Model.SetUpUserRoleComplete(game_OnSetUpUserRoleCompleted);
            game.OnDealCardBegins += new Model.DealCardBegin(game_OnDealCardBegins);
            game.OnShogunSelectionBegin += new ShogunSelectionBegin(game_OnShogunSelectionBegin);
        }

        /// <summary>
        /// 开始选择武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnShogunSelectionBegin(object sender, OgnasEventArgs.ShogunSelectArgs args)
        {

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
            Console.WriteLine(args.Messages);
            User ku = null;

            foreach (User ul in args.userList)
            {
                if (ul.UserRole == EnumUserRole.Lord)
                {
                    ku = ul;
                }
            }

            foreach (User u in args.userList)
            {
                ServerDealRoleProtocol drp = new ServerDealRoleProtocol();
                drp.ClientAddress = u.Address;
                drp.player = u;
                drp.playerKing = ku;
                SendMessage(drp);
            }
        }

        /// <summary>
        /// 座位分派结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnSetUpSeatCompleted(object sender, OgnasEventArgs.GameEventArgs args)
        {
            // TODO: 通知客户端座位结果
            Console.WriteLine(args.Messages);

            ServerDealSeatProtocol dsp = new ServerDealSeatProtocol();

            dsp.userList = args.userList;
            SendMessageAll(dsp, args.userList);
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

        /// <summary>
        /// 给全体玩家发送消息协议
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userList"></param>
        /// <returns></returns>
        internal byte[] SendMessageAll(Protocol message, List<User> userList)
        {
            foreach (var user in userList)
            {
                TcpClientUtils.SendData(user.Address, Constants.ClientPort, message);
            }
            return null;
        }

        /// <summary>
        /// 给单个玩家发送消息协议
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal byte[] SendMessage(Protocol message)
        {
            TcpClientUtils.SendData(message.ClientAddress, Constants.ClientPort, message);
            return null;
        }
    }
}
