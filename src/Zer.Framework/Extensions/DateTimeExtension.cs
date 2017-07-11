using System;
using System.Collections.Generic;

namespace Zer.Framework.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime[] GetDatesByHours(this DateTime start)
        {
            var list = new List<DateTime>();
            for (int i = 0; i <= 24; i++)
            {
                list.Add(start.AddHours(i));
            }
            return list.ToArray();
        }

        public static DateTime[] GetDatesByDays(this DateTime start, DateTime end)
        {
            //var list = new List<DateTime>();
            //var index = 0;
            //for (DateTime i = start; i <= end; i.AddDays(1))
            //{
            //    list.Add(i);
            //}
            //return list.ToArray();
            var span = Convert.ToInt32(Math.Abs(Math.Ceiling((start - end).TotalDays)));
            var list = new List<DateTime>();
            for (int i = 0; i < span; i++)
            {
                list.Add(start.AddDays(i).Date);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 计算指定时间变量与当前时间的距离，返回诸如3分钟前，刚刚，1小时前之类的时间间隔字符串
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string GetNowSpan(this DateTime datetime)
        {
            var now = DateTime.Now;
            var span = (now - datetime);
            
            if (span.TotalDays > 1) return string.Format("{0}天前", span.TotalDays.ToInt());
            if (span.TotalHours > 1) return string.Format("{0}小时前", span.TotalHours.ToInt());
            if (span.TotalMinutes > 1) return string.Format("{0}分钟前", span.TotalMinutes.ToInt());
            if(span.TotalSeconds>1) return string.Format("{0}秒前",span.TotalSeconds.ToInt());
            return "刚刚";
        }
    }
}