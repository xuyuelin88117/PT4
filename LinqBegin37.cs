// File: "LinqBegin37"
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
            Task("LinqBegin37");
            GetEnumerableString().Select((s, i) => s.Length != 0 ? s + (i + 1).ToString() : "").Where(s => s.Length != 0).OrderBy(s => s).Put();
        }
    }
}
