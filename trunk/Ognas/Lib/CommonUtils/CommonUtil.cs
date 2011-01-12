using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
