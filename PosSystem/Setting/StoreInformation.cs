using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PosSystem.Setting
{
    static class PosInformation
    {
        public static string year { get; set; }
        public static string storeName { get; set; }
        public static string regUserName { get; set; }
        public static void setYear()
        {
            DateTime dt = DateTime.Now;
            year = dt.ToString("yyyy").Substring(2);
        }
    }
}
