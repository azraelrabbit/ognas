using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ognas.Lib.Shoguns.Stunts;

namespace Ognas.Lib.Shoguns
{
    /// <summary>
    /// 武将
    /// </summary>
    public class Shogun
    {
        /// <summary>
        /// 武将代码
        /// </summary>
        public ShogunCode Code
        {
            get;
            set;
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex
        {
            get;
            set;
        }

        /// <summary>
        /// 阵营
        /// </summary>
        public Camp Camp
        {
            get;
            set;
        }

        /// <summary>
        /// 最大生命点数
        /// </summary>
        public int HealthMax
        {
            get;
            set;
        }

        /// <summary>
        /// 技能组
        /// </summary>
        public Dictionary<StuntCode, Stunt> StuntCollection
        {
            get;
            set;
        }

        /// <summary>
        /// 当前生命点数
        /// </summary>
        public int Health
        {
            get;
            set;
        }
    }
}
