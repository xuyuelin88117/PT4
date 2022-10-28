// File: "XFile70"
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

        public static bool iswinter(string s)
        {
            int mon = ((int)s[3] - 48) * 10 + (int)s[4] - 48;
            return (mon == 12 || mon == 1 || mon == 2) ? true : false;
        }
        
        public static void Solve()
        {
            Task("XFile70");
            FileStream f1 = File.OpenRead(GetString());
            FileStream f2 = File.OpenWrite(GetString());
            BinaryReader binf1 = new BinaryReader(f1);
            BinaryWriter binf2 = new BinaryWriter(f2);
            
            List<string> datalist = new List<string>();
            while(binf1.BaseStream.Position < binf1.BaseStream.Length)
            {
                string data = binf1.ReadString();
                Show(data);
                if(iswinter(data)) datalist.Add(data);
            }
            datalist.Reverse();
            foreach (string item in datalist)
            {
                binf2.Write(item);
            }

            binf1.Close();
            binf1.Dispose();
            f1.Close();
            f1.Dispose();
            binf2.Close();
            binf2.Dispose();
            f2.Close();
            f2.Dispose();
        }
    }
}
