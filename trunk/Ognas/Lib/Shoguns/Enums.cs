using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Shoguns
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 女
        /// </summary>
        Female,
        /// <summary>
        /// 男
        /// </summary>
        Male
    }

    /// <summary>
    /// 技能编码
    /// </summary>
    public enum StuntCode
    {
        LuoShen,
        NanManRuQin,
    }

    /// <summary>
    /// 阵营
    /// </summary>
    public enum Camp
    {
        /// <summary>
        /// 魏
        /// </summary>
        Wei,
        /// <summary>
        /// 蜀
        /// </summary>
        Shu,
        /// <summary>
        /// 吴
        /// </summary>
        Wu,
        /// <summary>
        /// 群雄
        /// </summary>
        Qun,
    }

    /// <summary>
    /// 武将代码
    /// </summary>
    public enum ShogunCode
    {
        /// <summary>
        /// 曹操，代码:01
        /// </summary>
        CaoCao = 1,
        ZhenJi = 2,
        XiaHouDun = 3,
        XuZhu = 4,
        ZhangLiao = 5,
        GuoJia = 6,
        SiMaYi = 7,
        XiaHouYuan = 8,
        CaoRen = 9,

        LiuBei = 33,
        MaChao = 34,
        HuangYueYing = 35,
        ZhaoYun = 36,
        ZhangFei = 37,
        GuanYu = 38,
        ZhuGeLiang = 39,
        HuangZhong = 40,
        WeiYan = 41,

        SunQuan = 65,
        LuXun = 66,
        ZhouYu = 67,
        DaQiao = 68,
        HuangGai = 69,
        LvMeng = 70,
        GanNing = 71,
        SunShangXiang = 72,
        XiaoQiao = 73,
        ZhouTai = 74,

        DiaoChan = 97,
        LvBu = 98,
        HuaTuo = 99,
        ZhangJiao = 100,
        YuJi = 101,
    }
}
