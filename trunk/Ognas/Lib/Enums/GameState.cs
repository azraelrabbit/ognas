using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Ognas.Lib.Enums
{
    public enum GameState : byte
    {
        [DescriptionAttribute("游戏开始")]
        GameStart,
        [DescriptionAttribute("初始化资源")]
        Inital,
        [DescriptionAttribute("洗牌")]
        ShuffleCard,
        [DescriptionAttribute("分派座位")]
        DealSeat,
        [DescriptionAttribute("分派身份")]
        DealRole,
        [DescriptionAttribute("开始发牌")]
        DealCardBegin,
        [DescriptionAttribute("给主公发牌")]
        DealCardLord,
        [DescriptionAttribute("给主公发牌")]
        DealCardLordComplete,
        [DescriptionAttribute("给除主公外的人发牌")]
        DealCardOther,
        [DescriptionAttribute("开始选择武将")]
        SelectShogunBegin,
        [DescriptionAttribute("主公选择武将")]
        SelectShogunLord,
        [DescriptionAttribute("主公选择武将完毕")]
        SelectShogunLordComplete,
        [DescriptionAttribute("其他人选择武将")]
        SelectShogunOther
    }
}
