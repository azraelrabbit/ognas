using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Protocals
{
    public class EnterRoomProtocal : Protocal
    {
        public EnterRoomProtocal()
        {
            this.SystemMessageEnum = Enum.SystemMessage.EnterRoom;
        }

        public EnterRoomProtocal(byte[] bytes)
        {
            this.SystemMessageEnum = Enum.SystemMessage.EnterRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.EnterRoom(this);
        }
    }
}
