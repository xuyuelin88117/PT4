// File: "LinqObj25"
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
        public static void Solve()
        {
            Task("LinqObj25");
            var file = File.ReadLines(GetString(), Encoding.Default)
            .Select(e =>
            {
                string[] str = e.Split(' ');
                return new
                {
                    s0 = str[0],
                    s1 = float.Parse(str[1]),
                    s2 = int.Parse(str[2])
                };
            })
            .GroupBy(e => ((e.s2 - 1) / 36 + 1))
            .Select(e => new { num = e.Key, sum = e.Sum(a => a.s1) });
            var res = file.Where(e => e.sum == file.Max(a => a.sum))
                          .Select(e => e.num + " " + e.sum);
            File.AppendAllLines(GetString(), res.ToArray(), Encoding.Default);
        }
    }
}
