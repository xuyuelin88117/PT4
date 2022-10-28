// File: "XString64"
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
            Task("XString64");
            string s = GetString();
            int K = GetInt();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= 'a' && s[i] <= 'z')
                {
                    if (s[i] - 'a' < K)
                    {
                        char c = s[i];
                        c = (char)(c + 26);
                        s = s.Remove(i, 1);
                        s = s.Insert(i, c.ToString());

                    }
                    char c2 = s[i];
                    c2 = (char)(c2 - K);
                    s = s.Remove(i, 1);
                    s = s.Insert(i, c2.ToString());
                }
                if (s[i] >= 'A' && s[i] <= 'Z')
                {
                    if (s[i] - 'A' < K)
                    {
                        char c = s[i];
                        c = (char)(c + 26);
                        s = s.Remove(i, 1);
                        s = s.Insert(i, c.ToString());
                    }
                    char c2 = s[i];
                    c2 = (char)(c2 - K);
                    s = s.Remove(i, 1);
                    s = s.Insert(i, c2.ToString());
                }
            }
            Put(s);

        }
    }
}
