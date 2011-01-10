using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    public class ZhenJi : Shogun
    {
        public ZhenJi()
        {
            base.Name = "甄姬";
            base.Sex = Shoguns.Sex.Female;
            base.Camp = Shoguns.Camp.Wei;
            base.HealthMax = 3;
        }
    }
}
