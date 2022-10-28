// File: "LinqBegin29"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqBegin29");
            var d = GetInt();
            var k = GetInt();
            var a = GetEnumerableInt();
            // var result = a.TakeWhile(x => x <= d).Union(a.Skip(k - 1)).OrderByDescending(x => x);
            // int count = 0;
            // foreach (var r in result)
            // {
            //     count++;
            // }
            // Put(count, result);

            var m = from num1 in a.TakeWhile(x => x <= d)
                    select num1;
            var n = from num2 in a.Skip(k - 1)
                    select num2;
            (from num3 in m.Union(n) orderby num3 descending select num3).Put();
        }
    }
}
