// File: "XString40"
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
            Task("XString40");
            string s = GetString();
            int position_1 = s.IndexOf(" ");
            int position_2 = s.LastIndexOf(" ");
            if (position_1 == position_2)
            {
                s = "";
            }
            else
            {
                s = s.Remove(position_2, s.Length - position_2);
                s = s.Remove(0, position_1 + 1);
            }
            Put(s);
        }
    }
}
