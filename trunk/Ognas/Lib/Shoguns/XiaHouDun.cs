using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class XiaHouDun:Shogun
    {
        public XiaHouDun()
        {
            base.Name = "夏侯惇";
            base.Sex = Shoguns.Sex.Male;
            base.HealthMax = 4;
            base.Camp = Shoguns.Camp.Wei;
        }
    }
}
