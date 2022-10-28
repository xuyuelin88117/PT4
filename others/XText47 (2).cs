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
            StreamReader sr = new StreamReader(GetString());
            int count = 0, sum = 0;           
            while(!sr.EndOfStream){
                string num;
                num = sr.ReadLine();
                ShowLine(num);
                if(num.LastIndexOf('.') == -1) {
                    count++;
                    sum += Convert.ToInt32(num);
                }
            }
            Put(count, sum);

        }
    }
}
