using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;
using System.Reflection;
using System.IO;
using System.Xml.Linq;
using System.Globalization;

namespace Ognas.Lib.Protocols
{
    public static class ProtocolFactory
    {
        private static Dictionary<SystemMessage, string> protocolDictionary = null;

        private static object protocolLock = new object();

        public static Protocol CreateProtocol(byte[] message)
        {
            SystemMessage systemMessage = (SystemMessage)message[0];
            return CreateProtocol(systemMessage, message.Skip(1).ToArray());
        }

        private static Protocol CreateProtocol(SystemMessage systemMessage, byte[] message)
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            if (null == protocolDictionary)
            {
                lock (protocolLock)
                {
                    protocolDictionary = new Dictionary<SystemMessage, string>();

                    using (Stream stream = assembly.GetManifestResourceStream(string.Format("{0}.{1}", assembly.GetName().Name, "ProtocolConfiguration.xml")))
                    {
                        XElement xElement = XElement.Load(stream);

                        foreach (var element in xElement.Elements())
                        {
                            protocolDictionary.Add((SystemMessage)System.Enum.Parse(typeof(SystemMessage), element.Attribute("EnumName").Value), element.Attribute("ClassName").Value);
                        }
                    }
                }
            }

            return (Protocol)assembly.CreateInstance(protocolDictionary[systemMessage], false, BindingFlags.CreateInstance, null, new object[] { message }, CultureInfo.InvariantCulture, null);
        }
    }
}
