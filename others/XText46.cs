// File: "XText46"
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
            Task("XText46");

            string fn1 = GetString();
            string fn2 = GetString();

            using (FileStream fs1 = new FileStream(fn1, FileMode.Open), 
                fs2 = new FileStream(fn2, FileMode.Create))
            {
                StreamReader sr = new StreamReader(fs1);
                BinaryWriter bw = new BinaryWriter(fs2);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    var words = line.Split(' ');
                    foreach (var word in words)
                    {
                        var a = double.TryParse(word, out var doubleVar);
                        int intVar = (int)doubleVar;
                        if (a && intVar != doubleVar)
                        {
                            bw.Write(doubleVar);
                        }
                    }
                }
                bw.Close();
            }
        }
    }
}
