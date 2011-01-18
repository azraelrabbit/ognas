using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Ognas.Lib.CommonUtils
{
    public static class CommonUtil
    {
        public static string GetOutputException(Exception ex)
        {
            return string.Format("Exception Message : {1} {0} Exception StackTrace{2}", Environment.NewLine, ex.Message, ex.StackTrace);
        }

        public static string GetOutputExceptionWithNewLine(Exception ex)
        {
            return string.Format("{0} {1}", Environment.NewLine, GetOutputException(ex));
        }

        public static string GetEnumDescription(object enumObject)
        {
            Type t = enumObject.GetType();
            string s = enumObject.ToString();
            DescriptionAttribute[] os = (DescriptionAttribute[])t.GetField(s).GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (os != null && os.Length == 1)
            {
                return os[0].Description;
            }
            return s;
        }
    }
}
