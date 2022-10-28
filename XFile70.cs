// File: "XFile70"
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
        public static bool func(string s)
        {
            int num = ((int)s[3] - 48) * 10 + (int)s[4] - 48;
            return (num == 12 || num == 1 || num == 2) ? true : false;
        }
        public static void Solve()
        {
            Task("XFile70");
            BinaryReader br = new BinaryReader(File.OpenRead(GetString()));
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(GetString()));
            
            List<string> list = new List<string>();
            while(br.BaseStream.Position < br.BaseStream.Length)
            {
                string s = br.ReadString();
                if(func(s)) list.Add(s);
            }
            list.Reverse();
            foreach (string i in list)
            {
                bw.Write(i);
            }

            br.Close();
            br.Dispose();
            bw.Close();
            bw.Dispose();
        }
    }
}
