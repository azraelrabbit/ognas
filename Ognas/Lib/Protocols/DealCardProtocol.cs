using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas;
using Ognas.Lib;
using Ognas.Lib.Skills;
using Ognas.Lib.Enums;

namespace Ognas.Lib.Protocols
{
    public class DealCardProtocol : Protocol
    {
        public List<Skill> cardList
        {
            get;
            set;
        }

        public User target
        {
            get;
            set;
        }
        public byte[] Message
        {
            get;
            set;
        }

        public GameState state
        {
            get;
            set;
        }

        public DealCardProtocol()
        {
            this.SystemMessageEnum = SystemMessage.DealCards;
        }

        public DealCardProtocol(byte[] message)
        {
            this.SystemMessageEnum = SystemMessage.DealCards;
            this.Message = message;
        }

        public void GetDealedCardsClient()
        {
            this.state = (GameState)this.Message[0];

            //if (this.state == GameState.DealCardLord)
            //{

            foreach (byte b in this.Message.Skip(1).ToArray())
            {
                Skill s = SkillUtility.GetSkill(Convert.ToInt32(b).ToString());
                this.cardList.Add(s);
            }
            //}

        }

        public void GetMessageDealdCardsServer()
        {
            List<byte> msg = new List<byte>();
            msg.Add((byte)this.SystemMessageEnum);
            msg.Add((byte)this.state);
            foreach (Skill skill in this.cardList)
            {
                msg.Add((byte)skill.SkillCode);
            }
            // msg.AddRange(Encoding.UTF8.GetBytes(this.target.UserName));
            this.Message = msg.ToArray();
        }
    }
}
