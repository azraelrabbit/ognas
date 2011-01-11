using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Shoguns.Stunts;

namespace Ognas.Lib.Shoguns
{
    public class Shogun
    {
        public ShogunCode Code
        {
            get;
            set;
        }

        public string Name
        {
            set;
            get;
        }

        public Sex Sex
        {
            get;
            set;
        }

        public Camp Camp
        {
            get;
            set;
        }

        public int HealthMax
        {
            get;
            set;
        }

        public Dictionary<StuntCode, Stunt> StuntCollection
        {
            get;
            set;
        }

        public int Health
        {
            get;
            set;
        }
    }
}
