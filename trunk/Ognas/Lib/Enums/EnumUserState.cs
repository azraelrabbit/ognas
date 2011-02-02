using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Ognas.Lib.Enums
{
    public enum EnumUserState : byte
    {
        [DescriptionAttribute("存活")]
        Live,
        [DescriptionAttribute("死亡")]
        Dead
    }
}
