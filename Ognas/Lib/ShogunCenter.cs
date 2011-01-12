using System;
using System.Collections.Generic;

using Ognas.Lib.Shoguns;

namespace Ognas.Lib
{
    public class ShogunCenter
    {
        private List<Shogun> _listShogun = new List<Shoguns.Shogun>();

        public List<Shogun> Shoguns
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

        public List<Shogun> GetSubShogunList(int count)
        {
            Dictionary<Shogun, int> dic = new Dictionary<Shogun, int>();
            dic.Add(_listShogun.Find(delegate(Shogun s) { return s.Code == ShogunCode.SunShangXiang; }), 50);

            return Utility.GetRandomList<Shogun>(_listShogun, dic, count);
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
                    // 为待主公选择武将列表添加3个必选项：刘备，孙权，曹操
                    Dictionary<Shogun,int> dic = new Dictionary<Shogun,int>();
                    dic.Add(_listShogun.Find(delegate(Shogun s) { return s.Code == ShogunCode.CaoCao; }), 100);
                    dic.Add(_listShogun.Find(delegate(Shogun s) { return s.Code == ShogunCode.LiuBei; }), 100);
                    dic.Add(_listShogun.Find(delegate(Shogun s) { return s.Code == ShogunCode.SunQuan; }), 100);
                    shogunList = Utility.GetRandomList<Shogun>(_listShogun, dic, 5);
                    break;
                default:
                    throw new Exception();
            }
            return shogunList;
        }
    }
}
