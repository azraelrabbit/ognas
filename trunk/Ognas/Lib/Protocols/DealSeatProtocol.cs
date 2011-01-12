using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class DealSeatProtocol : Protocol
    {
        public List<User> userList
        {
            get;
            set;
        }

        public string msgText
        {
            get;
            set;
        }
        public DealSeatProtocol()
        {
            this.SystemMessageEnum = SystemMessage.DealSeat;
        }

        public DealSeatProtocol(byte[] message)
        {
            this.SystemMessageEnum = SystemMessage.DealSeat;
            this.Data = Encoding.UTF8.GetString(message);
            this.GetUserSeatList();
        }

        public override byte[] RequestData
        {
            get
            {
                List<byte> byteList = new List<byte>();
                byteList.Add((byte)this.SystemMessageEnum);
                byteList.AddRange(Encoding.UTF8.GetBytes("|"));
                string strSendData = string.Empty;
                foreach (User u in userList)
                {
                    strSendData += u.UserName + "^";
                    strSendData += u.SeatNum + "^";
                    strSendData += ",";
                }
                if (!string.IsNullOrWhiteSpace(strSendData))
                {
                    strSendData = strSendData.Remove(strSendData.LastIndexOf("^"), 1);
                    strSendData = strSendData.Remove(strSendData.LastIndexOf(","), 1);
                }
                strSendData += "|";
                byteList.AddRange(Encoding.UTF8.GetBytes(strSendData));
                this.Data = Encoding.UTF8.GetString(byteList.ToArray());
                return byteList.ToArray();
            }
        }

        public List<User> GetUserSeatList()
        {
            string[] datalist = this.Data.Split('|');
            // this.SystemMessageEnum = (SystemMessage)(Encoding.UTF8.GetBytes(datalist[0])[0]);

            string[] users = datalist[1].Split(',');
            this.userList = new List<User>();
            for (int i = 0; i < users.Length; i++)
            {
                string[] us = users[i].Split('^');
                User u = new User(us[0]);
                u.SeatNum = us[1];
                this.userList.Add(u);
            }

            this.msgText = datalist[2];

            return this.userList;
        }

        public override byte[] ResponseData
        {
            get
            {

                return base.ResponseData;
            }
        }

        public override byte[] OnResponse()
        {
            //throw new NotImplementedException();
            return Encoding.UTF8.GetBytes(this.Data);
        }
    }
}
