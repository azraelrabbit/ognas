using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Ognas.Lib.Skills
{
    public class SkillUtility
    {
        public static Skill GetSkill(string code)
        {
            Skill skill = new Skill();
            skill.Code = code;

            string SkillInfo = Ognas.Resource.Skills.ResourceManager.GetString(code);
            if (string.IsNullOrEmpty(SkillInfo))
            {
                return null;
            }

            string[] sinfo = SkillInfo.Split(new string[1] { "|" }, StringSplitOptions.None);
            skill.SkillCode = (SkillCode)int.Parse(sinfo[0].Substring(2), NumberStyles.AllowHexSpecifier);
            skill.Name = sinfo[1];
            skill.CardColor = (Skills.CardColor)int.Parse(sinfo[2].Substring(2), NumberStyles.AllowHexSpecifier);
            skill.CardNumber = (Skills.CardNumber)int.Parse(sinfo[3].Substring(2), NumberStyles.AllowHexSpecifier);
            //skill.Range = int.Parse(sinfo[4]);

            return skill;
        }

        public static List<Skill> GetSkillList()
        {

            List<Skill> listSkill = new List<Skill>();

            for (int i = 96; i <= 255; i++)
            {
                string code = Convert.ToString(i, 16).ToUpper();
                Skill skill = GetSkill(code);
                if (Object.Equals(null, skill))
                {
                    continue;
                }

                listSkill.Add(skill);
            }

            return listSkill;
        }
    }
}
