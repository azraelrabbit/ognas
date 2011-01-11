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
            InitialShoguns();
        }

        public void InitialShoguns()
        {
            if (_listShogun == null)
            {
                _listShogun = new List<Shogun>();
            }

            _listShogun.Clear();

            _listShogun.Add(new CaoCao());
            _listShogun.Add(new ZhenJi());
            _listShogun.Add(new XiaHouDun());
            _listShogun.Add(new XuChu());
            _listShogun.Add(new ZhangLiao());
            _listShogun.Add(new LiuBei());
            _listShogun.Add(new SunQuan());
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
                    Utility.RemoveItemsFromList<Shogun>(_listShogun, _listShogun.FindAll(delegate(Shogun s) { return s.Name == "曹操" || s.Name == "孙权" || s.Name == "刘备"; }));
                    shogunList = Utility.GetRandomList<Shogun>(_listShogun, 2);

                    shogunList.Add(new CaoCao());
                    shogunList.Add(new LiuBei());
                    shogunList.Add(new SunQuan());
                    break;
                default:
                    throw new Exception();
            }
            shogunList.Reverse();
            return shogunList;
        }
    }
}
