using System;

namespace Hotel.Web_MVC.Utils
{
    public class StringUtils
    {
        public static Boolean IsBlank(string s)
        {
            return s == null || s.Trim().Equals("");
        }
    }
}
