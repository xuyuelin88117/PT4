// File: "LinqObj20"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //初始序列包含有关申请人的信息。 每个序列元素包括以下字段：
    //<姓氏> <学校编号> <入学年份>
    //确定哪些学校所有年份的申请人总数最多，并显示这些学校的申请人数据（先注明学校编号，然后注明申请人的姓氏）。
    //每个申请人的信息应显示在新行上，并按学校编号的升序排序，对于相同的编号 - 按原始数据集中的申请人顺序排列。
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj20");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        name = s[0],
                        code = int.Parse(s[1]),
                    };
                }).OrderBy(e => e.code).ToArray();//GroupBy(e => e.code).
            var q = (from p in r group p by p.code into g orderby g.Count() descending select g).ToList();
            int a = q.First().Count();
            var c = (from p in q where p.Count() == a select p).ToList().Show();
            var loj = (from e in r
                       join b in c
                       on e.code equals b.Key
                       select new
                       {
                           e.code,
                           e.name,
                        }).Select(e => e.code + " " + e.name).ToList();
            File.WriteAllLines(GetString(), loj.ToArray(), Encoding.Default);
        }
    }
}
