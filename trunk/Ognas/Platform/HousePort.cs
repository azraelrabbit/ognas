using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Platform
{
    public class HousePort
    {
        #region Tcp Port
        private static Dictionary<int, bool> tcpPortDictionary = new Dictionary<int, bool>();

        private static int unusedTcpPortCount = 0;

        private static object tcpPortLock = new object();

        private static int currentMaxTcpPort = Convert.ToInt32(ConfigurationManager.AppSettings["InstanceBeginTcpPort"]);

        public static int GetHouseTcpPort()
        {
            lock (tcpPortLock)
            {
                if (unusedTcpPortCount < 1)
                {
                    int port = currentMaxTcpPort;
                    tcpPortDictionary.Add(port, true);
                    currentMaxTcpPort++;
                    return port;
                }
                else
                {
                    foreach (var port in tcpPortDictionary.Keys)
                    {
                        if (!tcpPortDictionary[port])
                        {
                            unusedTcpPortCount--;
                            return port;
                        }
                    }
                    throw new ApplicationException("Some error occurred in tcp port subroutine");
                }
            }
        }
        #endregion

        #region Udp Port
        private static Dictionary<int, bool> udpPortDictionary = new Dictionary<int, bool>();

        private static int unusedUdpPortCount = 0;

        private static object udpPortLock = new object();

        private static int currentMaxUdpPort = Convert.ToInt32(ConfigurationManager.AppSettings["InstanceBeginUdpPort"]);

        public static int GetHouseUdpPort()
        {
            lock (udpPortLock)
            {
                if (unusedUdpPortCount < 1)
                {
                    int port = currentMaxUdpPort;
                    udpPortDictionary.Add(port, true);
                    currentMaxUdpPort++;
                    return port;
                }
                else
                {
                    foreach (var port in udpPortDictionary.Keys)
                    {
                        if (!udpPortDictionary[port])
                        {
                            unusedUdpPortCount--;
                            return port;
                        }
                    }
                    throw new ApplicationException("Some error occurred in udp port subroutine");
                }
            }
        }
        #endregion
    }
}
