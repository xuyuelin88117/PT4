// File: "XText47"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XText47");
            StreamReader f = new StreamReader(GetString());
            int count = 0, res = 0;
            string num = string.Empty;
            while(!f.EndOfStream)
            {
                num = f.ReadLine();
                ShowLine(num);
                if(num.LastIndexOf('.') == -1) 
                {
                    ShowLine(num);
                    count++;
                    res += Convert.ToInt32(num);
                }
            }
            Put(count, res);
        }
    }
}
