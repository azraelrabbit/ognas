using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Enum;

namespace Platform.Protocals
{
    public static class ProtocalFactory
    {
        public static Protocal CreateProtocal(byte[] message)
        {
            SystemMessage systemMessage = (SystemMessage)message[0];
            return CreateProtocal(systemMessage, message.Skip(1).ToArray());
        }

        private static Protocal CreateProtocal(SystemMessage systemMessage, byte[] message)
        {
            Protocal protocal = null;
            switch (systemMessage)
            {
                case SystemMessage.CreateRoom:
                    protocal = new CreateRoomProtocal(message);
                    break;
                case SystemMessage.EnterRoom:
                    protocal = new EnterRoomProtocal(message);
                    break;
                case SystemMessage.RegisterUserName:
                    protocal = new RegisterUserProtocal(message);
                    break;
                case SystemMessage.UdpMessage:
                    protocal = new UdpMessageProtocal(message);
                    break;
                case SystemMessage.ExitRoom:
                    protocal = new ExitRoomProtocal(message);
                    break;
                case SystemMessage.DealSeat:
                    protocal = new DealSeatProtocal(message);
                    break;
                case SystemMessage.DealRole:
                    protocal = new DealRoleProtocal(message);
                    break;
                default:
                    break;
            }
            return protocal;
        }
    }
}
