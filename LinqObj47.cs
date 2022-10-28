// File: "LinqObj47"
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
            Task("LinqObj47");
            var r = File.ReadLines(GetString(), Encoding.Default).Select(x =>
               {
                   var arr = x.Split(' ');
                   return new
                   {
                       priceof1Liter = arr[0],
                       company = arr[1],
                       street = arr[2],
                       gasolineBrand = arr[3]
                   };
               }).GroupBy(x => new { company = x.company, street = x.street })
               .Select(x => new { company = x.Key.company, street = x.Key.street, count = x.Count() })
               .Where(x => x.count >= 2)
               .OrderBy(x => x.company)
               .ThenBy(x => x.street)
               .ToList();
            string s = GetString();
            if (r.Count > 0)
            {
                File.WriteAllLines(s, r.Select(x => $"{x.company} {x.street} {x.count}"));
            }
            else
            {
                File.WriteAllText(s, "No");
            }
        }
    }
}
