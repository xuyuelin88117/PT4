// File: "XText42"
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
        public static async void Solve()
        {
            Task("XText42");
            string s;
            double a, b;
            int n;
            a = GetDouble(); b = GetDouble(); n = GetInt();
            s = GetString();
            FileStream fs = new FileStream(s, FileMode.Create,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            for(int i = 0; i <= n; i++){
                double d = a + i * ((b - a) / n);
                double sqrt = Math.Sqrt(d);
                string str = d.ToString("f4");
                string sq = sqrt.ToString("f8");
                str = "    " + str + "     " + sq + "\n";
                sw.Write(str);
            }
            sw.Close();
            fs.Close();
        }
    }
}
