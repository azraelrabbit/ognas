using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Protocals
{
    public class CreateRoomProtocal : Protocal
    {
        public CreateRoomProtocal()
        {
            this.SystemMessageEnum = Enum.SystemMessage.CreateRoom;
        }

        public CreateRoomProtocal(byte[] bytes)
        {
            this.SystemMessageEnum = Enum.SystemMessage.CreateRoom;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.CreateRoomThread(this);
        }
    }
}
