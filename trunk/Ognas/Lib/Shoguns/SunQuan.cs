using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class SunQuan:Shogun
    {
        public SunQuan() {
            base.Name = "孙权";
            base.Sex = Shoguns.Sex.Male;
            base.Camp = Shoguns.Camp.Wu;
            base.HealthMax = 4;
        }
    }
}
