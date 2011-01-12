using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Protocols;

namespace Platform.Protocols
{
    public class ServerRegisterUserProtocol : RegisterUserProtocol
    {
        public ServerRegisterUserProtocol() : base()
        {
        }

        public override byte[] OnResponse()
        {
            MainFrame mainFrame = (MainFrame)this.Host;
            return mainFrame.RegisterUser(this);
        }
    }
}
