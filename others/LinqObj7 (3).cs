// File: "LinqObj7"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        // To read strings from the source text file into
        // a string sequence (or array) s, use the statement:
        //   s = File.ReadLines(GetString());
        // To write the sequence s of IEnumerable<string> type
        // into the resulting text file, use the statement:
        //   File.WriteAllLines(GetString(), s);
        // When solving tasks of the LinqObj group, the following
        // additional methods defined in the taskbook are available:
        // (*) Show() and Show(cmt) (extension methods) - debug output
        //       of a sequence, cmt - string comment;
        // (*) Show(e => r) and Show(cmt, e => r) (extension methods) -
        //       debug output of r values, obtained from elements e
        //       of a sequence, cmt - string comment.

        //给定一个整数 K——健身中心的一位客户的代码。源序列包含有关该健身中心客户的信息。每个序列元素包括以下整数字段：
        //<课程持续时间（以小时为单位）> <年份> <月数> <客户代码>
        //对于代码为 K 的客户访问中心的每一年，确定该客户今年上课时间最长的月份（如果有多个这样的月份，则选择数字最小的月份）。
        //按以下顺序在新行上显示每年的信息：年、月数、本月的课程持续时间。
        //按年份编号的降序对信息进行排序。如果没有关于代码 K 的客户端的数据，则将字符串“No data”写入结果文件。
        public static void Solve()
        {
            Task("LinqObj7");
            int K = GetInt();

            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        hours = int.Parse(s[0]),
                        year = int.Parse(s[1]),
                        month = int.Parse(s[2]),
                        code = int.Parse(s[3])
                    };
                }).Where(e => e.code == K).ToList()
                  .OrderByDescending(e => e.hours).ThenByDescending(e => e.year).ThenBy(e => e.month)
                  .ToList().GroupBy(e => new { e.year })
                  .Select(e => e.FirstOrDefault()).OrderByDescending(e => e.year)
                  .Select(e => e.year + " " + e.month + " " + e.hours);
            if(r.Count() != 0)
                File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
            else
            {
                string S = "No Data";
                File.WriteAllText(GetString(), S, Encoding.Default);
            }

        }

    }
}

