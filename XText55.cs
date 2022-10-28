// File: "XText55"
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
            Task("XText55");
            StreamReader r = new StreamReader(GetString());
            FileStream f = File.OpenWrite(GetString());
            BinaryWriter br = new BinaryWriter(f);

            SortedSet<char> symbolist = new SortedSet<char>();
            while (!r.EndOfStream)
            {
                char c = ((char)r.Read());
                if (!symbolist.Contains(c))
                {
                    symbolist.Add(c);
                }
            }
            foreach (char c in symbolist)
            {
                if ((int)c != 10 && (int)c != 13)
                    br.Write(c);
            }
            r.Close();
            br.Close();
            f.Close();
        }
    }
}
