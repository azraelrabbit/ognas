using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Shoguns;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class SelectionShogunProtocol : Protocol
    {
        public List<Shogun> shoGunList
        {
            get;
            set;
        }

        public Shogun SelectedShogun
        {
            get;
            set;
        }

        public User currentUser
        {
            get;
            set;
        }

        public string msgText
        {
            get;
            set;
        }

        public byte[] message
        {
            get;
            set;
        }

        public Enums.GameState state
        {
            get;
            set;
        }


        public SelectionShogunProtocol()
        {
            this.SystemMessageEnum = SystemMessage.SelectShogun;
        }

        public SelectionShogunProtocol(List<Shogun> sglist, Enums.GameState st)
        {
            this.state = st;
            this.SystemMessageEnum = SystemMessage.SelectShogun;
            this.shoGunList = sglist;
            this.GetShogunMessage();
        }

        public SelectionShogunProtocol(Shogun selectedShogun, User user, Enums.GameState st)
        {
            this.state = st;
            this.SystemMessageEnum = SystemMessage.SelectShogun;
            this.SelectedShogun = selectedShogun;
            this.currentUser = user;
            this.GetShogunMessageClient();
        }
        public void GetShogunMessageClient()
        {
            if (this.SelectedShogun != null)
            {
                List<byte> byteList = new List<byte>();
                byteList.Add((byte)this.SystemMessageEnum);
                byteList.Add((byte)this.state);
                byteList.Add((byte)this.SelectedShogun.Code);
                byteList.AddRange(Encoding.UTF8.GetBytes(this.currentUser.UserName));
                this.message = byteList.ToArray();
            }
        }
        public SelectionShogunProtocol(byte[] message)
        {
            this.message = message;
            this.shoGunList = new List<Shogun>();
            // GetSelectedShogun();
        }

        private void GetShogunMessage()
        {
            if (this.shoGunList != null)
            {
                List<byte> byteList = new List<byte>();
                byteList.Add((byte)this.SystemMessageEnum);
                byteList.Add((byte)this.state);
                foreach (Shogun sg in this.shoGunList)
                {

                    byteList.Add((byte)sg.Code);
                }
                this.message = byteList.ToArray();
            }
        }

        public void GetMessageShogun()
        {
            if (this.message != null && this.message.Length > 0)
            {
                this.state = (Enums.GameState)this.message[0];
                foreach (byte b in this.message.Skip(1).ToArray())
                {
                    Shogun sg = ShogunUtility.GetShogun((ShogunCode)b);
                    this.shoGunList.Add(sg);
                }
            }
        }

        public void GetSelectedShogun()
        {
            this.state = (Enums.GameState)this.message[0];
            this.SelectedShogun = ShogunUtility.GetShogun((ShogunCode)message[1]);
            this.currentUser = new User(Encoding.UTF8.GetString(this.message.Skip(2).ToArray()).Trim());
            this.currentUser.Address = this.ClientAddress;
        }

        public override byte[] RequestData
        {
            get
            {
                return this.message;
            }
        }

        public override byte[] ResponseData
        {
            get
            {
                return message;
            }
            //set
            //{
            //    message = value;
            //}
        }

    }
}
