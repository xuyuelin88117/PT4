// File: "XText36"
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
            Task("XText36");
            string s1 = GetString(), s2 = "tmp.txt";
            StreamReader rf = new StreamReader(s1);
            StreamWriter wf = new StreamWriter(s2);
            while (rf.Peek() != -1)
            {
                string line = string.Empty;
                line = rf.ReadLine();
                int num = 0;
                if (line != "")
                {
                    while (true)
                    {
                        if (Char.IsWhiteSpace(line[num]))
                        {
                            num++;
                        }
                        else
                            break;
                    }
                }
                if (num % 2 == 0)
                {
                    line = line.Remove(0, num / 2);
                    wf.WriteLine(line);
                }
                else
                {
                    line = line.Remove(0, num / 2 + 1);
                    wf.WriteLine(line);
                }

            }
            rf.Close();
            wf.Close();
            File.Delete(s1);
            File.Move(s2, s1);
        }
    }
}
