using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ognas.Lib.Shoguns
{
    public class ShogunUtility
    {
        public static Shogun GetShogun(ShogunCode code)
        {
            Shogun s = new Shogun();

            SetProperties(s,ShogunBasicInfoResource.ResourceManager.GetString(Enum.GetName(typeof(ShogunCode), code)));

            return s;
        }

        private static void SetProperties(Shogun s, string info)
        {
            string[] sinfo = info.Split(new string[1] { "|" }, StringSplitOptions.None);
            s.Code = (ShogunCode)int.Parse(sinfo[0].Substring(2), NumberStyles.AllowHexSpecifier);
            s.Name = sinfo[1];
            s.Sex = (Shoguns.Sex)int.Parse(sinfo[2].Substring(2), NumberStyles.AllowHexSpecifier);
            s.Camp = (Camp)int.Parse(sinfo[3].Substring(2), NumberStyles.AllowHexSpecifier);
            s.HealthMax = int.Parse(sinfo[4]);
        }

        public static List<Shogun> GetShogunList()
        {
            List<Shogun> listShogun = new List<Shogun>();

            foreach (ShogunCode code in Enum.GetValues(typeof(ShogunCode)))
            {
                listShogun.Add(GetShogun(code));
            }

            return listShogun;
        }
    }
}
