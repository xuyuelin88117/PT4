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
            var r = File.ReadLines(GetString(), Encoding.Default)
            .Select(e =>
            {
                string[] s = e.Split(' ');
                return new
                {
                    name = s[0],
                    val = float.Parse(s[1]),
                    id = int.Parse(s[2])
                };
            }).GroupBy(e => ((e.id-1) / 36 + 1)).Show().Select(e => new { num = e.Key, sum = e.Sum(a => a.val) }).Show();
            var ans = r.Where(e => e.sum == r.Max(a => a.sum)).Select(e => e.num + " " + e.sum);

            File.AppendAllLines(GetString(), ans.ToArray(), Encoding.Default);
        }
    }
}
