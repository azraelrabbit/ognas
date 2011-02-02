using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using Platform.Model;
using Platform.OgnasEventArgs;

namespace Platform
{
    public static class ProtocolProcess
    {
        public static byte[] CreateRoomProtocolResponse(object host, Protocol protocol)
        {
            MainFrame mainFrame = (MainFrame)host;
            return mainFrame.CreateRoomThread(protocol);
        }

        public static byte[] EnterRoomProtocolResponse(object host, Protocol protocol)
        {
            MainFrame mainFrame = (MainFrame)host;
            return mainFrame.EnterRoom(protocol);
        }

        public static byte[] ExitRoomProtocolResponse(object host, Protocol protocol)
        {
            Room room = (Room)host;
            return room.UserExit(protocol);
        }

        public static byte[] RegisterUserProtocolResponse(object host, Protocol protocol)
        {
            MainFrame mainFrame = (MainFrame)host;
            return mainFrame.RegisterUser(protocol);
        }

        public static byte[] UdpMessageProtocolResponse(object host, Protocol protocol)
        {
            Room room = (Room)host;
            string userName = room.AddressUserDictionary[protocol.ClientAddress].UserName;
            protocol.Data = string.Format("{0} : {1}", userName, protocol.Data);
            return room.SendUdpMessage(protocol.Data);
        }

        public static byte[] SelectionShogunProtocalResponse(object host, Protocol protocol)
        {
            // 接收到客户端发送的协议对象((SelectionShogunProtocol)protocol)
            // 激发选择武将事件

            SelectionShogunProtocol ssp = ((SelectionShogunProtocol)protocol);
            ssp.GetSelectedShogun();
            ShogunSelectArgs ssa = new ShogunSelectArgs();
            ssa.ssProtocol = ssp;
            Console.WriteLine(Ognas.Lib.CommonUtils.CommonUtil.GetEnumDescription(ssp.state));
            Console.WriteLine(ssp.currentUser.UserName + " 选择的武将是: " + ssp.SelectedShogun.Name);
            Room room = (Room)host;

            // 激发主公武将选择完成事件
            room.gameBase.SelectedShogunLoardCompleted(room, ssa);
            return new byte[0];
        }
    }
}
