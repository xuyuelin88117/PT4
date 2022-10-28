// File: "XString48"
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
            Task("XString48");//
            string s = GetString();
            int m;
            int p;
            s = s + " ";
            m = 0;
            while (true)
            {
                if (s[m].Equals(' '))
                {
                    m++;
                }
                else
                    break;
            }
            p = s.IndexOf(" ");
            for (int i = m; i <= p - 1; i++)
            {
                if (s[i] == s[m] && i != m)
                {
                    s = s.Remove(i, 1);
                    s = s.Insert(i, ".");
                }
                if (i == p - 1)
                {
                    m = s.IndexOf(" ", p + 1);
                    if(m == -1){
                        break;
                    }
                    while (true)
                    {
                        if (s[m].Equals(' '))
                        {
                            m++;
                        }
                        else
                            break;
                    }
                    i = m;
                    p = s.IndexOf(" ", m);
                }
            }
            s = s.Remove(s.Length-1 , 1);
            Put(s);
        }
    }
}
