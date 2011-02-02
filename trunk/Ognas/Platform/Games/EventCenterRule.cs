using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;
using System.Collections;
using LibProtocols = Ognas.Lib.Protocols;
using Ognas.Lib.Protocols;
using Ognas.Lib;
using Ognas.Lib.Shoguns;
using Ognas.Lib.Enums;
using Ognas.Lib.SocketUtils;
using Ognas.Lib.Skills;

namespace Platform.Games
{
    public class EventCenterRule
    {
        Room room;

        Game game;

        public EventCenterRule(object objgame)
        {
            game = (Game)objgame;
            game.OnShuffleCardCompleted += new Model.ShuffleCardComplete(game_OnShuffleCardCompleted);
            game.OnSetUpSeatCompleted += new Model.SetUpSeatComplete(game_OnSetUpSeatCompleted);
            game.OnSetUpUserRoleCompleted += new Model.SetUpUserRoleComplete(game_OnSetUpUserRoleCompleted);
            game.OnDealCardBegins += new Model.DealCardBegin(game_OnDealCardBegins);
            game.OnShogunSelectionBegin += new ShogunSelectionBegin(game_OnShogunSelectionBegin);
            game.OnShogunSelectionLordCompleted += new ShogunSelectionLordComplete(game_OnShogunSelectionLordCompleted);
            game.OnShogunSelectionOtherBegin += new ShogunSelectionOtherBegin(game_OnShogunSelectionOtherBegin);
        }

        // 其他玩家选择武将
        void game_OnShogunSelectionOtherBegin(object sender, OgnasEventArgs.ShogunSelectArgs args)
        {

            SelectionShogunProtocol ssp = new SelectionShogunProtocol();

        }

        // 主公武将选择完成
        void game_OnShogunSelectionLordCompleted(object sender, OgnasEventArgs.ShogunSelectArgs args)
        {
            Room r = (Room)sender;
            //args.ssProtocol.SelectedShogun
            User user = this.game.userList.Find(new Predicate<User>(delegate(User u)
            {
                return u.Address == args.ssProtocol.ClientAddress;
            }));
            user.shogun = args.SelectedShogun;
            game.SelectedShogunOtherBegin();
        }

        /// <summary>
        /// 开始选择武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnShogunSelectionBegin(object sender, OgnasEventArgs.ShogunSelectArgs args)
        {
            // 首先由主公选择武将
            this.game.gameState = GameState.SelectShogunLord;
            Console.WriteLine(Ognas.Lib.CommonUtils.CommonUtil.GetEnumDescription(this.game.gameState));
            List<Shogun> shoGunLord = args.shogunCenter.GetSubShogunList(TypeofInitialShogunList.ForMaster);

            User lord;
            // lord = args.userList.Find(new Predicate<User>(targ));
            lord = game.NextUser();
            SelectionShogunProtocol ssp = new SelectionShogunProtocol(shoGunLord, GameState.SelectShogunLord);
            ssp.ClientAddress = lord.Address;
            this.SendMessage(ssp);
        }

        bool targ(User u)
        {
            return (u.UserRole == EnumUserRole.Lord);
        }

        /// <summary>
        /// 开始发牌
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void game_OnDealCardBegins(object sender, OgnasEventArgs.DealCardsArgs args)
        {
            // TODO: 开始发牌
            int countLord = 5;
            int countOther = 3;
            game.gameState = GameState.DealCardLord;
            foreach (User u in args.userList)
            {
                if (u.UserRole == EnumUserRole.Lord)
                {
                    List<Skill> cardLord = Utility.GetRandomList(args.cardList, countLord);
                    u.HandCards = cardLord;
                    DealCardProtocol dcp = new DealCardProtocol();
                    dcp.cardList = cardLord;
                    dcp.state = GameState.DealCardLord;
                    dcp.ClientAddress = u.Address;

                    this.SendMessage(dcp);
                    Utility.RemoveItemsFromList(args.cardList, cardLord);
                }
                else
                {
                    List<Skill> cardOther = Utility.GetRandomList(args.cardList, countOther);
                    u.HandCards = cardOther;
                    DealCardProtocol dcp = new DealCardProtocol();
                    dcp.cardList = cardOther;
                    dcp.state = GameState.DealCardOther;
                    dcp.ClientAddress = u.Address;
                    this.SendMessage(dcp);
                    Utility.RemoveItemsFromList(args.cardList, cardOther);

                }

            }


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
                DealRoleProtocol drp = new DealRoleProtocol();
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

            DealSeatProtocol dsp = new DealSeatProtocol();

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
