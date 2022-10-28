// File: "XText35"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/*
XText35°. A text file whose lines are left-aligned is given. 
Make the given text centered by means of adding leading blank characters to all nonempty lines. 
The width of text must be equal to 50 characters. 
If the length of line is an odd number then add one blank character to the beginning of this line before centering.
*/
namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XText35");
            string s = GetString();
            string s0 = "tmp";
            StreamReader rf = new StreamReader(s);
            StreamWriter wf = new StreamWriter(s0);
            while(rf.Peek() != -1)
            {
                string line = string.Empty;
                line = rf.ReadLine();
                if (line.Length % 2 == 1)
                {
                    line = " " + line;
                }
                if (line.Length == 0)
                {
                    wf.WriteLine(line);
                    continue;
                }
                string blankS = new string(' ',(50 - line.Length) / 2);
                line = blankS + line;
                wf.WriteLine(line);
            }
            rf.Close();
            wf.Close();
            File.Delete(s);
            File.Move(s0, s);
        }
    }
}
