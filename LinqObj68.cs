// File: "LinqObj68"
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
            Task("LinqObj68");
            var r = File.ReadLines(GetString(), Encoding.Default).Select(x =>
               {
                   var arr = x.Split(' ');
                   return new
                   {
                       Class = Convert.ToInt32(arr[0]),
                       Grade = Convert.ToInt32(arr[1]),
                       LastName = arr[2],
                       Initials = arr[3],
                       ItemName = arr[4],
                   };
               }).GroupBy(x => new { Class = x.Class, LastName = x.LastName, Initials = x.Initials })
                 .Where(x => x.Any(m => m.Grade >= 4) && x.All(m => !m.Grade.Equals(2) || !m.Grade.Equals(3)))
                 .Select(x => new { Class = x.Key.Class, LastName = x.Key.LastName, Initials = x.Key.Initials, Count = x.Count() })
                 .OrderBy(x => x.Count)
                 .OrderBy(x => x.Initials)
                 .ThenBy(x => x.LastName)
                 .ToList();
            string s = GetString();
            if (r.Count > 0)
            {
                File.WriteAllLines(s, r.Select(x => $"{x.Count} {x.LastName} {x.Initials} {x.Class}"));
            }
            else
            {
                File.WriteAllText(s, "No");
            }
        }
    }
}
