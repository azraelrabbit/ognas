using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Ognas.Lib.Enums
{
    public enum EnumUserRole : byte
    {
        /// <summary>
        /// 主公
        /// </summary>
        [DescriptionAttribute("主公")]
        Lord,
        /// <summary>
        /// 忠臣
        /// </summary>
        [DescriptionAttribute("忠臣")]
        Loyal,
        /// <summary>
        /// 反贼
        /// </summary>
        [DescriptionAttribute("反贼")]
        rebel,
        /// <summary>
        /// 内奸
        /// </summary>
        [DescriptionAttribute("内奸")]
        Traitor
    }
}
