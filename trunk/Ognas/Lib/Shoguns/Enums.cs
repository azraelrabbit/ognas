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
        /// 男
        /// </summary>
        Male = 0x01,
        /// <summary>
        /// 女
        /// </summary>
        Female = 0x02,
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
        Wei = 0x01,
        /// <summary>
        /// 蜀
        /// </summary>
        Shu = 0x02,
        /// <summary>
        /// 吴
        /// </summary>
        Wu = 0x03,
        /// <summary>
        /// 群雄
        /// </summary>
        Qun = 0x04,
    }

    /// <summary>
    /// 武将代码
    /// </summary>
    public enum ShogunCode
    {
        /// <summary>
        /// 0x01 曹操
        /// </summary>
        CaoCao          = 0x01,
        /// <summary>
        /// 甄姬
        /// </summary>
        ZhenJi          = 0x02,
        /// <summary>
        /// 夏侯惇
        /// </summary>
        XiaHouDun       = 0x03,
        /// <summary>
        /// 许褚
        /// </summary>
        XuChu           = 0x04,
        /// <summary>
        /// 张辽
        /// </summary>
        ZhangLiao       = 0x05,
        /// <summary>
        /// 郭嘉
        /// </summary>
        GuoJia          = 0x06,
        /// <summary>
        /// 司马懿
        /// </summary>
        SiMaYi          = 0x07,
        /// <summary>
        /// 夏侯渊
        /// </summary>
        XiaHouYuan      = 0x08,
        /// <summary>
        /// 曹仁
        /// </summary>
        CaoRen          = 0x09,

        /// <summary>
        /// 刘备
        /// </summary>
        LiuBei          = 0x21,
        /// <summary>
        /// 马超
        /// </summary>
        MaChao          = 0x22,
        /// <summary>
        /// 黄月英
        /// </summary>
        HuangYueYing    = 0x23,
        /// <summary>
        /// 赵云
        /// </summary>
        ZhaoYun         = 0x24,
        /// <summary>
        /// 张飞
        /// </summary>
        ZhangFei        = 0x25,
        /// <summary>
        /// 关羽
        /// </summary>
        GuanYu          = 0x26,
        /// <summary>
        /// 诸葛亮
        /// </summary>
        ZhuGeLiang      = 0x27,
        /// <summary>
        /// 黄忠
        /// </summary>
        HuangZhong      = 0x28,
        /// <summary>
        /// 魏延
        /// </summary>
        WeiYan          = 0x29,

        /// <summary>
        /// 孙权
        /// </summary>
        SunQuan         = 0x41,
        /// <summary>
        /// 陆逊
        /// </summary>
        LuXun           = 0x42,
        /// <summary>
        /// 周瑜
        /// </summary>
        ZhouYu          = 0x43,
        /// <summary>
        /// 大乔
        /// </summary>
        DaQiao          = 0x44,
        /// <summary>
        /// 黄盖
        /// </summary>
        HuangGai        = 0x45,
        /// <summary>
        /// 吕蒙
        /// </summary>
        LvMeng          = 0x46,
        /// <summary>
        /// 甘宁
        /// </summary>
        GanNing         = 0x47,
        /// <summary>
        /// 孙尚香
        /// </summary>
        SunShangXiang   = 0x48,
        /// <summary>
        /// 小乔
        /// </summary>
        XiaoQiao        = 0x49,
        /// <summary>
        /// 周泰
        /// </summary>
        ZhouTai         = 0x4A,

        /// <summary>
        /// 貂蝉
        /// </summary>
        DiaoChan        = 0x61,
        /// <summary>
        /// 吕布
        /// </summary>
        LvBu            = 0x62,
        /// <summary>
        /// 华佗
        /// </summary>
        HuaTuo          = 0x63,
        /// <summary>
        /// 张角
        /// </summary>
        ZhangJiao       = 0x64,
        /// <summary>
        /// 于吉
        /// </summary>
        YuJi            = 0x65,
    }
}
