using System;
using System.Collections;

namespace KidsPos.Sources.Util
{
    public class Time
    {
        private readonly Hashtable _dow = new Hashtable();

        public Time()
        {
            _dow.Add(DayOfWeek.Sunday, "日");
            _dow.Add(DayOfWeek.Monday, "月");
            _dow.Add(DayOfWeek.Tuesday, "火");
            _dow.Add(DayOfWeek.Wednesday, "水");
            _dow.Add(DayOfWeek.Thursday, "木");
            _dow.Add(DayOfWeek.Friday, "金");
            _dow.Add(DayOfWeek.Saturday, "土");
        }

        public string GetTime()
        {
            var dt = DateTime.Now;
            return dt.ToString("yyyy年MM月dd日(" + _dow[dt.DayOfWeek] + ") HH時mm分ss秒");
        }
    }
}