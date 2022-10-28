// File: "LinqBegin45"
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
            Task("LinqBegin45");
            var l1 = GetInt();
            var l2 = GetInt();
            GetEnumerableString().Where(s => s.Length == l1).Concat(GetEnumerableString().Where(s => s.Length == l2)).OrderByDescending(s => s).Put();
        }
    }
}
