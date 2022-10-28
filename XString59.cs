// File: "XString59"
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
            Task("XString59");
            string s = GetString();
            string res = "";
            int i = s.LastIndexOf('/');
            int r = s.LastIndexOf('.');
            // int r = s.LastIndexOf('.');
            if (r >= 0 && i < r)
            {
                res = s.Remove(0, r + 1);
                Put(res);
            }
            else
                Put(res);

        }
    }
}
