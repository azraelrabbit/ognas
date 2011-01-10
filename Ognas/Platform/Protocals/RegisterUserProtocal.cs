using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Platform.Protocals
{
    public class RegisterUserProtocal : Protocal
    {
        public RegisterUserProtocal()
        {
            this.SystemMessageEnum = Enum.SystemMessage.RegisterUserName;
        }

        public RegisterUserProtocal(byte[] bytes) 
        {
            this.SystemMessageEnum = Enum.SystemMessage.RegisterUserName;
            this.Data = Encoding.UTF8.GetString(bytes);
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.RegisterUser(this);
        }
    }
}
