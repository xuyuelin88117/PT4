// File: "LinqObj48"
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
        //源序列包含有关加油站（加油站）的信息。每个序列元素包括以下字段：
        //<1 升价格（戈比）> <街道> <汽油等级> <公司>
        //公司名称和街道名称不包含空格。 92、95、98号为汽油牌号，每家公司在每条街道上不超过一个加油站；
        //同一家公司的不同加油站的价格可能会有所不同。枚举原始数据集中包含的街道和公司的所有可能组合，
        //并为每对“street-company”输出街道名称、公司名称和该公司位于该街道的加油站提供的汽油品牌数（如果没有加油站，则假定数量为 0）。
        //在新行上显示有关每对的信息，并按字母顺序按街道名称排序，对于相同的街道名称 - 按公司名称（也按字母顺序）排序。
        public static void Solve()
        {
            Task("LinqObj48");
            var all = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        sc = s[1] + " " + s[3],
                        street = s[1],
                        company = s[3],
                        number = int.Parse(s[2]),
                    };
                }).OrderBy(e => e.street).ThenBy(e => e.company).ToArray();
            var a = all.GroupBy(e => e.sc, (key, ee) => new { key, count = ee.Count() }).ToArray();
            var street = all.Select(e => e.street).GroupBy(e => e).Select(e => e.FirstOrDefault()).OrderBy(e => e).ToArray();
            var company = all.Select(e => e.company).GroupBy(e => e).Select(e => e.FirstOrDefault()).OrderBy(e => e).ToArray();
            var allkinds = street.SelectMany(e => company.Select(b => e + " " + b)).Select(e => new { sc = e, count = 0 }).ToArray();
            var inner = (from u in allkinds
                         join g in a
                         on u.sc equals g.key into G
                         from aa in G.DefaultIfEmpty()
                         select new { name = u.sc, count = (aa == null) ? 0 : aa.count })
                      .Select(e => e.name + " " + e.count.ToString());
            File.WriteAllLines(GetString(), inner.ToArray(), Encoding.Default);
        }
    }
}
