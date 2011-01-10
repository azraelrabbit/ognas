using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class ZhangLiao:Shogun
    {
        public ZhangLiao()
        {
            base.Name = "张辽";
            base.Sex = Shoguns.Sex.Male;
            base.HealthMax = 4;
            base.Camp = Shoguns.Camp.Wei;
        }
    }
}
