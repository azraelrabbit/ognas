using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class XuZhu:Shogun
    {
        public XuZhu()
        {
            base.Camp = Shoguns.Camp.Wei;
            base.Name = "许褚";
            base.Sex = Shoguns.Sex.Male;
            base.HealthMax = 4;
        }
    }
}
