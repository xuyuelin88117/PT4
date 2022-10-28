// File: "XText49"
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
            Task("XText49");
            string s1 = GetString(), s2 = "tmp.txt";
            StreamReader rf = new StreamReader(s1);

            FileStream f = File.OpenRead(GetString());
            BinaryReader binf = new BinaryReader(f);
            StreamWriter wf = new StreamWriter(s2);
            string s = string.Empty;
            int n = 0;
            while(!rf.EndOfStream)
            {
                s = rf.ReadLine();
                if(binf.BaseStream.Position < binf.BaseStream.Length)
                {
                    n = binf.ReadInt32();
                    s += n.ToString();
                }
                wf.WriteLine(s);
            }
            rf.Close();
            f.Close();
            wf.Close();
            File.Delete(s1);
            File.Move(s2, s1);
        }
    }
}
