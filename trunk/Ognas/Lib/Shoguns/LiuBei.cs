using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class LiuBei:Shogun
    {
        public LiuBei()
        {
            base.Name = "刘备";
            base.Sex = Shoguns.Sex.Male;
            base.Camp = Shoguns.Camp.Shu;
            base.HealthMax = 4;
        }


    }
}
