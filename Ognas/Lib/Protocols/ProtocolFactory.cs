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
        private static Dictionary<SystemMessage, ProtocolStruct> protocolDictionary = null;

        private static object protocolLock = new object();

        public static Protocol CreateProtocol(Assembly currentAssembly, byte[] message)
        {
            SystemMessage systemMessage = (SystemMessage)message[0];
            return CreateProtocol(currentAssembly, systemMessage, message.Skip(1).ToArray());
        }

        private static Protocol CreateProtocol(Assembly currentAssembly, SystemMessage systemMessage, byte[] message)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            if (null == protocolDictionary)
            {
                lock (protocolLock)
                {
                    if (null == protocolDictionary)
                    {
                        protocolDictionary = new Dictionary<SystemMessage, ProtocolStruct>();                        

                        using (Stream stream = entryAssembly.GetManifestResourceStream(ConfigurationManager.AppSettings["ProtocalResource"]))
                        {
                            XElement xElement = XElement.Load(stream);

                            foreach (var element in xElement.Elements())
                            {
                                SystemMessage systemMessageEnum = (SystemMessage)System.Enum.Parse(typeof(SystemMessage), element.Attribute("EnumName").Value);
                                ProtocolStruct protocolStruct = new ProtocolStruct();
                                protocolStruct.ClassName = element.Attribute("ClassName").Value;
                                protocolStruct.ProcessClass = element.Attribute("ProcessClass").Value;
                                protocolStruct.MethodName = element.Attribute("MethodName").Value;
                                protocolDictionary.Add(systemMessageEnum, protocolStruct);
                            }
                        }
                    }
                }
            }
            Assembly callingAssembly = Assembly.GetCallingAssembly();

            Protocol protocol = (Protocol)callingAssembly.CreateInstance(protocolDictionary[systemMessage].ClassName, false, BindingFlags.CreateInstance, null, new object[] { message }, CultureInfo.InvariantCulture, null);

            if(!string.IsNullOrWhiteSpace(protocolDictionary[systemMessage].ProcessClass)
                && !string.IsNullOrWhiteSpace(protocolDictionary[systemMessage].MethodName))
            {
                Type processClass = currentAssembly.GetType(protocolDictionary[systemMessage].ProcessClass);
                MethodInfo method = processClass.GetMethod(protocolDictionary[systemMessage].MethodName);
                protocol.OnResponseDelegate = (ResponseDelegate)Delegate.CreateDelegate(typeof(ResponseDelegate), method);
            }

            return protocol;
        }
    }
}
