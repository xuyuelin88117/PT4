// File: "XFile55"
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
            Task("XFile55");
            string s0 = GetString();
            FileStream f0 = File.OpenWrite(s0);
            BinaryWriter binf0 = new BinaryWriter(f0);
            int N = GetInt();
            FileStream f;
            BinaryReader binf;
            int num = 0;
            for (int i = 0; i < N;i++)
            {
                f = File.OpenRead(GetString());
                binf = new BinaryReader(f);
                // ShowLine(binf.BaseStream.Length / sizeof(int));
                binf0.Write(((int)(binf.BaseStream.Length / sizeof(int))));
                while(binf.BaseStream.Position < binf.BaseStream.Length)
                {
                    num = binf.ReadInt32();
                    Show(num);
                    binf0.Write(num);
                }
                binf.Close();
                binf.Dispose();
                f.Close();
                f.Dispose();
            }
            binf0.Close();
            binf0.Dispose();
            f0.Close();
            f0.Dispose();
        }
    }
}
