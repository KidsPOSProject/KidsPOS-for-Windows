using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace PosSystem.Util
{
    public class Time
    {
        private Hashtable dow = new Hashtable();
        public Time()
        {
            dow.Add(DayOfWeek.Sunday, "日");
            dow.Add(DayOfWeek.Monday, "月");
            dow.Add(DayOfWeek.Tuesday, "火");
            dow.Add(DayOfWeek.Wednesday, "水");
            dow.Add(DayOfWeek.Thursday, "木");
            dow.Add(DayOfWeek.Friday, "金");
            dow.Add(DayOfWeek.Saturday, "土");
        }

        public string getTime()
        {
            DateTime dt = DateTime.Now;
            return dt.ToString("yyyy年MM月dd日(" + dow[dt.DayOfWeek] + ") HH時mm分ss秒");
        }
    }
}
