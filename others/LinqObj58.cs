// File: "LinqObj58"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //初始序列包含有关数学、俄语和计算机科学学生通过 USE 的结果的信息（按指定顺序）。每个序列元素包括以下字段：
    //<姓氏> <姓名首字母> <学校编号> <USE分数>
    //USE 分数是 0 到 100 范围内的三个整数，它们之间用一个空格隔开。
    //对于每所学校，找出至少一门科目得分低于 50 分的前三名学生（按字母顺序），并输出他们的姓氏、姓名首字母和学校编号。
    //在单独的行上显示每个学生的信息，并按姓氏和首字母的字母顺序排序，如果匹配，则按升序学校编号。
    //如果某个学校满足指定条件的学生少于三个，则显示所有此类学生的信息。
    //如果初始集中没有一个学生满足指定条件，则将文本“Students not found”写入结果文件。
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj58");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        name = s[0],
                        nm = s[1],
                        school = int.Parse(s[2]),
                        score1 = int.Parse(s[3]),
                        score2 = int.Parse(s[4]),
                        score3 = int.Parse(s[5]),
                    };
                }).Where(e => e.score1 < 50 || e.score2 < 50 || e.score3 < 50)
                  .OrderBy(e => e.name).ThenBy(e => e.nm)
                  .GroupBy(e => e.school).Show()
                  .SelectMany(x => x.OrderBy(y => y.name).Take(3))
                  .OrderBy(e => e.name).ThenBy(e => e.nm).ThenBy(e => e.school)
                  .Select(e => e.name + " " + e.nm + " " + e.school);


            if (r.Count() != 0)
                File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
            else
            {
                string S = "Required students not found";
                File.WriteAllText(GetString(), S, Encoding.Default);
            }
        }
    }
}
