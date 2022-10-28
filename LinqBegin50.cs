// File: "LinqBegin50"
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
            Task("LinqBegin50");
            GetEnumerableString().GroupJoin(GetEnumerableString(), x => x[0], y => y[0], (x, y) => string.Format("{0}:{1}", x, y.Count())).Put();
        }
    }
}
