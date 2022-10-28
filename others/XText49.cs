// File: "XText49"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//XText49°. A text file and a binary file of integers are given. 
// Add a string representation of each integer 
//from the binary file to the end of the corresponding line of the text file. 
//If the amount of integers is less than the amount of text lines then do not change remaining text lines.


namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XText49");
            string name = GetString();
            StreamReader rf = new StreamReader(name);

            FileStream f = File.OpenRead(GetString());
            BinaryReader binf = new BinaryReader(f);
            StreamWriter wf = new StreamWriter("tmp");
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
            File.Delete(name);
            File.Move("tmp", name);

        }
    }
}
