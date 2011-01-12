using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Skills
{
    public enum SkillCode
    {
        /*--------基本牌-----------*/
        /// <summary>
        /// 0x01 杀
        /// </summary>
        Sha = 0x01,

        /// <summary>
        /// 0x02 闪
        /// </summary>
        Shan = 0x02,

        /// <summary>
        /// 0x03 桃
        /// </summary>
        Tao = 0x03,

        /*--------锦囊牌-----------*/
        /// <summary>
        /// 0x04 闪电
        /// </summary>
        ShanDian = 0x04,

        /// <summary>
        /// 0x05 乐不思蜀
        /// </summary>
        LeBuSiShu = 0x05,

        /// <summary>
        /// 0x06 无懈可击
        /// </summary>
        WuXieKeJi = 0x06,

        /// <summary>
        /// 0x07 借刀杀人
        /// </summary>
        JieDaoShaRen = 0x07,

        /// <summary>
        /// 0x08 五谷丰登
        /// </summary>
        WuGuFengDeng = 0x08,

        /// <summary>
        /// 0x09 无中生有
        /// </summary>
        WuZhongShengYou = 0x09,

        /// <summary>
        /// 0x0A 决斗
        /// </summary>
        JueDou = 0x0A,

        /// <summary>
        /// 0x0B 桃园结义
        /// </summary>
        TaoYuanJieYi = 0x0B,

        /// <summary>
        /// 0x0C 南蛮入侵
        /// </summary>
        NanManRuQin = 0x0C,

        /// <summary>
        /// 0x0D 万箭齐发
        /// </summary>
        WanJianQiFa = 0x0D,

        /// <summary>
        /// 0x0E 顺手牵羊
        /// </summary>
        ShunShouQianYang = 0x0E,

        /// <summary>
        /// 0x0F 过河拆桥
        /// </summary>
        GuoHeChaiQiao = 0x0F,

        /*--------装备牌-----------*/
        /// <summary>
        /// 0x10 爪黄飞电
        /// </summary>
        ZhuaHuangFeiDian = 0x10,

        /// <summary>
        /// 0x11 的卢
        /// </summary>
        DiLu = 0x11,

        /// <summary>
        /// 0x12 绝影
        /// </summary>
        JueYing = 0x12,

        /// <summary>
        /// 0x13 赤兔
        /// </summary>
        ChiTu = 0x13,

        /// <summary>
        /// 0x14 紫锥
        /// </summary>
        ZiZhui = 0x14,

        /// <summary>
        /// 0x15 大宛
        /// </summary>
        DaWan = 0x15,

        /// <summary>
        /// 0x16 诸葛连弩
        /// </summary>
        ZhuGeLianNu = 0x16,

        /// <summary>
        /// 0x17 寒冰剑
        /// </summary>
        HanBingJian = 0x17,

        /// <summary>
        /// 0x18 青釭剑
        /// </summary>
        QingGangJian = 0x18,

        /// <summary>
        /// 0x19 雌雄双剑
        /// </summary>
        CiXiongShuangJian = 0x19,

        /// <summary>
        /// 0x1A 贯石斧
        /// </summary>
        GuanShiFu = 0x1A,

        /// <summary>
        /// 0x1B 青龙偃月刀
        /// </summary>
        QingLongYanYueDao = 0x1B,

        /// <summary>
        /// 0x1C 丈八蛇矛
        /// </summary>
        ZhangBaSheMao = 0x1C,

        /// <summary>
        /// 0x1D 方天画戟
        /// </summary>
        FangTianHuaJi = 0x1D,

        /// <summary>
        /// 0x1E 麒麟弓
        /// </summary>
        QiLinGong = 0x1E,

        /// <summary>
        /// 0x1F 八卦阵
        /// </summary>
        BaGuanZhen = 0x1F,

        /// <summary>
        /// 0x20 仁王盾
        /// </summary>
        RenWangDun = 0x20,
    }

    /// <summary>
    /// 技能牌子类型代码
    /// </summary>
    public enum SkillType
    {
        /// <summary>
        /// 0x01 基本牌
        /// </summary>
        JiBen = 0x01,

        /// <summary>
        /// 0x02 锦囊牌
        /// </summary>
        JiNang = 0x02,

        /// <summary>
        /// 0x03 装备牌
        /// </summary>
        ZhuangBei = 0x03,
    }

    /// <summary>
    /// 花色
    /// </summary>
    public enum CardColor
    {
        /// <summary>
        /// 0x01 黑桃
        /// </summary>
        Spade = 0x01,

        /// <summary>
        /// 0x02 红桃
        /// </summary>
        Heart = 0x02,

        /// <summary>
        /// 0x03 梅花
        /// </summary>
        Club = 0x03,

        /// <summary>
        /// 0x04 方块
        /// </summary>
        Diamond = 0x04,
    }

    /// <summary>
    /// 花数
    /// </summary>
    public enum CardNumber
    {
        /// <summary>
        /// 0x01 A
        /// </summary>
        Ace = 0x01,

        /// <summary>
        /// 0x02 2
        /// </summary>
        Two = 0x02,

        /// <summary>
        /// 0x03 3
        /// </summary>
        Three = 0x03,

        /// <summary>
        /// 0x04 4
        /// </summary>
        Four = 0x04,

        /// <summary>
        /// 0x05 5
        /// </summary>
        Five = 0x05,

        /// <summary>
        /// 0x06 6
        /// </summary>
        Six = 0x06,

        /// <summary>
        /// 0x07 7
        /// </summary>
        Seven = 0x07,

        /// <summary>
        /// 0x08 8
        /// </summary>
        Eight = 0x08,

        /// <summary>
        /// 0x09 9
        /// </summary>
        Nine = 0x09,

        /// <summary>
        /// 0x0A 10
        /// </summary>
        Ten = 0x0A,

        /// <summary>
        /// 0x0B J
        /// </summary>
        Jack = 0x0B,

        /// <summary>
        /// 0x0C Q
        /// </summary>
        Queen = 0x0C,

        /// <summary>
        /// 0x0D K
        /// </summary>
        King = 0x0D
    }
}
