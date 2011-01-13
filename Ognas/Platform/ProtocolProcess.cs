using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;
using Platform.Model;

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
    }
}
