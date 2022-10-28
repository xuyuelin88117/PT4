// File: "XString30"
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
            Task("XString30");
            char C = GetChar();
            string s = GetString();
            string s0 = GetString();
            int position = 0;
            //position=s.find(C);
            //s.insert(position+1,s0);
            while (position <= s.LastIndexOf(C))
            {
                position = s.IndexOf(C, position);
                Show(position);
                //cout<<"position  "<<i<<" : "<<position<<endl;
                s = s.Insert(position + 1, s0);
                position++;
            }
            Put(s);
        }
    }
}
