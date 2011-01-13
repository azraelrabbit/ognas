using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Globalization;
using System.Configuration;

namespace Ognas.Lib.Protocols
{
    public static class ProtocolFactory
    {
        private static Dictionary<SystemMessage, string> protocolDictionary = null;

        private static object protocolLock = new object();

        public static Protocol CreateProtocol(Assembly currentAssembly, byte[] message)
        {
            SystemMessage systemMessage = (SystemMessage)message[0];
            return CreateProtocol(currentAssembly, systemMessage, message.Skip(1).ToArray());
        }

        private static Protocol CreateProtocol(Assembly currentAssembly, SystemMessage systemMessage, byte[] message)
        {
            if (null == protocolDictionary)
            {
                lock (protocolLock)
                {
                    if (null == protocolDictionary)
                    {
                        protocolDictionary = new Dictionary<SystemMessage, string>();

                        Assembly assembly = Assembly.GetEntryAssembly();

                        using (Stream stream = assembly.GetManifestResourceStream(ConfigurationManager.AppSettings["ProtocalResource"]))
                        {
                            XElement xElement = XElement.Load(stream);

                            foreach (var element in xElement.Elements())
                            {
                                protocolDictionary.Add((SystemMessage)System.Enum.Parse(typeof(SystemMessage), element.Attribute("EnumName").Value), element.Attribute("ClassName").Value);
                            }
                        }
                    }
                }
            }

            return (Protocol)currentAssembly.CreateInstance(protocolDictionary[systemMessage], false, BindingFlags.CreateInstance, null, new object[] { message }, CultureInfo.InvariantCulture, null);
        }
    }
}
