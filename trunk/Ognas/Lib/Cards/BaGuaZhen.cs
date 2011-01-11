using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Cards
{
    /// <summary>
    /// 八卦阵
    /// </summary>
    public class BaGuaZhen : EquipsCard
    {

        public BaGuaZhen()
        {
            this.EquipmentClass = EquipmentClasses.Armor;
            this.Remark = "防具类装备.装备[八卦阵]后，每次需要出[闪]时（例如受到[杀]或[万箭齐发]攻击时），可以选择判定，若判定结果为红色花色（红桃或方块），则等效于出了一张[闪]；否则需再出[闪]。";
        }
    }
}
