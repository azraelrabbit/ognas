using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.Model;

namespace Platform.Protocals
{
    public class ExitRoomProtocal : Protocal
    {
        public ExitRoomProtocal()
        {
            this.SystemMessageEnum = Enum.SystemMessage.ExitRoom;
        }

        public ExitRoomProtocal(byte[] bytes)
        {
            this.SystemMessageEnum = Enum.SystemMessage.ExitRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            Room room = (Room)Host;
            return room.UserExit(this);
        }
    }
}
