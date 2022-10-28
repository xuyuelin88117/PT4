// File: "XText55"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
// XText55°. A text file is given. 
//Create a new binary file of characters that contains all characters of the given text (without repetitions) 
//including blank character and punctuation marks. 
//The characters must be in ascending order of their numeric values in the character set.


namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XText55");
            StreamReader tf = new StreamReader(GetString());
            FileStream f = File.OpenWrite(GetString());
            BinaryWriter wf = new BinaryWriter(f);

            SortedSet<char> symbolist = new SortedSet<char>();
            while (!tf.EndOfStream)
            {
                char c = ((char)tf.Read());
                if (!symbolist.Contains(c))
                {
                    ShowLine(c);
                    symbolist.Add(c);
                }
            }
            foreach (char c in symbolist)
            {
                if((int)c == 10 || (int)c == 13)
                    continue;
                wf.Write(c);
            }
            tf.Close();
            wf.Close();
            f.Close();
        }


    }
}
