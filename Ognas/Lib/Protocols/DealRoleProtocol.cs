using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Enums;
using Ognas.Lib.Enum;

namespace Ognas.Lib.Protocols
{
    public class DealRoleProtocol : Protocol
    {
        public User playerKing
        {
            get;
            set;
        }

        public User player
        {
            get;
            set;
        }

        public string msgText
        {
            get;
            set;
        }

        public DealRoleProtocol()
        {
            this.SystemMessageEnum = SystemMessage.DealRole;
        }

        public DealRoleProtocol(byte[] message)
        {
            this.SystemMessageEnum = SystemMessage.DealRole;
            this.Data = Encoding.UTF8.GetString(message);
            GetUserRoleList();
        }

        public override byte[] RequestData
        {
            get
            {
                List<byte> byteList = new List<byte>();
                byteList.Add((byte)this.SystemMessageEnum);
                byteList.AddRange(Encoding.UTF8.GetBytes("|"));
                string strSendData = string.Empty;
                strSendData += player.UserName + "^";
                strSendData += player.SeatNum + "^";
                List<byte> pr = new List<byte>();
                pr.Add((byte)player.UserRole);
                strSendData += Encoding.UTF8.GetString(pr.ToArray()) + "^";
                strSendData += ",";
                strSendData += playerKing.UserName + "^";
                strSendData += playerKing.SeatNum + "^";
                List<byte> kr = new List<byte>();
                kr.Add((byte)playerKing.UserRole);
                strSendData += Encoding.UTF8.GetString(kr.ToArray());
                strSendData += "|";
                strSendData += this.msgText;

                byteList.AddRange(Encoding.UTF8.GetBytes(strSendData));
                this.Data = Encoding.UTF8.GetString(byteList.ToArray());
                return byteList.ToArray();
            }
        }

        public void GetUserRoleList()
        {
            string[] datalist = this.Data.Split('|');
            //this.SystemMessageEnum = (SystemMessage)(Encoding.UTF8.GetBytes(datalist[0])[0]);

            string[] users = datalist[1].Split(',');

            string[] p = users[0].Split('^');
            string[] k = users[1].Split('^');

            this.player = new User(p[0]);
            this.player.SeatNum = p[1];
            this.player.UserRole = (enumUserRole)Encoding.UTF8.GetBytes(p[2])[0];

            this.playerKing = new User(k[0]);
            this.playerKing.SeatNum = k[1];
            this.playerKing.UserRole = (enumUserRole)Encoding.UTF8.GetBytes(k[2])[0];
            this.msgText = datalist[2];
        }

        public override byte[] OnResponse()
        {
            return Encoding.UTF8.GetBytes(this.Data);
        }
    }
}
