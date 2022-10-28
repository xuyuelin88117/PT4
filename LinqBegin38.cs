// File: "LinqBegin38"
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
            Task("LinqBegin38");
            GetEnumerableInt().Where((_, i) => i % 3 != 2).Select((x, i) => i % 2 == 0 ? 2 * x : x).Put();

        }
    }
}
