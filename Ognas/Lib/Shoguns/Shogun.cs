using System.Collections.Generic;
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

        /// <summary>
        /// 卡牌图片
        /// </summary>
        public string Picture
        {
            get;
            set;
        }

        public Shogun()
        { }



        #region Override Operators
        public static bool operator ==(Shogun s1,Shogun s2)
        {
            if ((object)s1 == null || (object)s2 == null)
            {
                return (object)s1 == null && (object)s2 == null;
            }

            return s1.Code == s2.Code;
        }

        public static bool operator !=(Shogun s1, Shogun s2)
        {
            return !(s1 == s2);
        }
        public override bool Equals(object obj)
        {
            if (obj is Shogun)
            {
                return this.Code == ((Shogun)obj).Code;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

    }
}
