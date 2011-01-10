using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    /// <summary>
    /// 曹操
    /// </summary>
    public class CaoCao : Shogun
    {
        /// <summary>
        /// 曹操
        /// </summary>
        public CaoCao()
        {
            base.Name = "曹操";
            base.Sex = Shoguns.Sex.Male;
            base.Camp = Shoguns.Camp.Wei;
            base.HealthMax = 4;
        }
    }
}
