// File: "XText42"
using PT4;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XText42");
            double a = GetDouble(), b = GetDouble(), step_size;
            int N = GetInt();
            FileStream fs = new FileStream(GetString(), FileMode.Create,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            step_size = (b - a) / N;
            for (int i = 0; i <= N; i++)
            {
                double d1 = a + i * step_size;
                double d2 = Math.Sqrt(a + i * step_size);
                string res1 = d1.ToString("#0.0000");
                string res2 = d2.ToString("#0.00000000");
                string res = "    " + res1 + "     " + res2 + "\n";
                sw.Write(res);
            }
            sw.Close();
        }
    }
}
