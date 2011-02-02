using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Ognas.Lib.Enums
{
    public enum EnumAroundState : byte
    {
        [DescriptionAttribute("回合开始")]
        AroundStart,
        [DescriptionAttribute("回合结束")]
        AroundOver

    }
}
