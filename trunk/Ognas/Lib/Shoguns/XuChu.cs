using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class XuChu : Shogun
    {
        public XuChu()
        {
            base.Camp = Shoguns.Camp.Wei;
            base.Name = "许褚";
            base.Sex = Shoguns.Sex.Male;
            base.HealthMax = 4;
        }
    }
}
