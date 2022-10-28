// File: "XText46"
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
            Task("XText46");
            StreamReader f = new StreamReader(new FileStream(GetString(), FileMode.Open));
            BinaryWriter bw = new BinaryWriter(new FileStream(GetString(), FileMode.Create));
            while (!f.EndOfStream)
            {
                string line = f.ReadLine();
                var number = line.Split(' ');
                foreach (var num in number)
                {
                    var dou_num = double.TryParse(num, out var dou_var);
                    int int_var = (int)dou_var;
                    if (dou_num && int_var != dou_var)
                    {
                        bw.Write(dou_var);
                    }
                }
            }
            bw.Close();
        }

    }
}