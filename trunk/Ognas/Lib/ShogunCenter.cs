using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Ognas.Lib.Shoguns;

namespace Ognas.Lib
{
    public class ShogunCenter
    {
        private List<Shogun> _listShogun = new List<Shoguns.Shogun>();

        public List<Shogun> AllShoguns
        {
            get
            {
                return _listShogun;
            }

            private set
            {
                ;
            }
        }

        public ShogunCenter() 
        {
            _listShogun = ShogunUtility.GetShogunList();
        }

        public List<Shogun> GetSubShogunList(TypeofInitialShogunList tisl)
        {
            List<Shogun> shogunList = new List<Shogun>();
            switch (tisl)
            {
                case TypeofInitialShogunList.ForPlayer:
                    shogunList = Utility.GetRandomList<Shogun>(_listShogun, 3);
                    Utility.RemoveItemsFromList<Shogun>(_listShogun, shogunList);
                    break;
                case TypeofInitialShogunList.ForMaster:
                    // 为主公发武将选择牌
                    // 获取全部武将列表
                    List<Shogun> list = ShogunUtility.GetShogunList();
                    // 从该列表中移除曹操，孙权，刘备
                    Utility.RemoveItemsFromList<Shogun>(list, _listShogun.FindAll(delegate(Shogun s) { return s.Code == ShogunCode.CaoCao || s.Code == ShogunCode.LiuBei || s.Code == ShogunCode.SunQuan; }));
                    // 从该列表中随机选取两名武将生成待主公选择武将列表
                    shogunList = Utility.GetRandomList<Shogun>(list, 2);

                    // 为待主公选择武将列表添加3个必选项：刘备，孙权，曹操
                    shogunList.Add(ShogunUtility.GetShogun(ShogunCode.LiuBei));
                    shogunList.Add(ShogunUtility.GetShogun(ShogunCode.SunQuan));
                    shogunList.Add(ShogunUtility.GetShogun(ShogunCode.CaoCao));
                    break;
                default:
                    throw new Exception();
            }
            // 反转待选择武将列表
            shogunList.Reverse();
            return shogunList;
        }
    }
}
