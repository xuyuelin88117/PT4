// File: "XString8"
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
            Task("XString8");
            int N;
            N = GetInt();
            char C;
            C = GetChar();
            string s = "";
            for (int i = 0; i < N; i++)
            {
                s += C;
            }
            Put(s);
        }
    }
}
