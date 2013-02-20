using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin
{
    class ExceptionSerializer
    {
        public static String ToString(Exception exc)
        {
            return ToString(exc, "==++==");
        }
        public static String ToString(Exception exc, string sep)
        {
            StringBuilder text = new StringBuilder();
            while (exc != null)
            {
                if (text.Length > 0) text.Append(sep);
                text.Append(exc.Message);
                exc = exc.InnerException;
            }
            return text.ToString();
        }
    }
}
