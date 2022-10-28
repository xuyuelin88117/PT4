// File: "XString16"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static async void Solve()
        {
            Task("XString16");
            string s, s1 = "";
            s = GetString();
            foreach (char c in s)
            {
                if (c <= 'Z' && c >= 'A')
                {
                    s1 += (char)(c + 32);
                }
                else
                {
                    s1 += c;
                }
            }
            Show(s1);
            // for (int counter = 0; counter < s.Length; counter++)
            // {
            //     if(s[counter] < 'Z' && s[counter] > 'A'){
            //         s1 += (char)(s[counter] + 32);
            //     } else {
            //         s1 += s[counter];
            //     }
            // }
            Put(s1);
        }
    }
}
