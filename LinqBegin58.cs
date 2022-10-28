// File: "LinqBegin58"
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
            Task("LinqBegin58");
            GetEnumerableString().GroupBy(c => c[0], (a, s) => s.OrderByDescending(e => e.Length).First()).OrderBy(c => c[0]).Put();
        }
    }
}
