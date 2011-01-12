using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Shoguns.Stunts;

namespace Ognas.Lib.Skills
{
    public class Skill
    {
        public string Code
        {
            get;
            set;
        }

        public SkillCode SkillCode
        {
            get;
            set;
        }

        public string Name
        {
            set;
            get;
        }

        public CardColor CardColor
        {
            set;
            get;
        }

        public CardNumber CardNumber
        {
            set;
            get;
        }

        public SkillType SkillType
        {
            set;
            get;
        }

        public int Range
        {
            set;
            get;
        }

        public Stunt Stunt
        {
            get;
            set;
        }
    }
}
