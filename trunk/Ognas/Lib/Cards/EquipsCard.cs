﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ognas.Lib.Cards
{
    /// <summary>
    /// 装备类卡
    /// </summary>
    public abstract class EquipsCard : Card
    {
        public EquipmentClasses EquipmentClass
        {
            get;
            set;
        }
    }
}