// File: "LinqBegin26"
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
            Task("LinqBegin26");
            int k1, k2;
            (k1, k2) = GetInt2();
            var a = GetEnumerableString();
            Put(a.Take(k1-1).Union(a.Skip(k2).Take(a.Count()-k2)).Select(e=>e.Length).Average()); 
        }
    }
}
