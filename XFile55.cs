// File: "XFile55"
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
            Task("XFile55");
            BinaryWriter bw = new BinaryWriter(File.OpenWrite(GetString()));
            int N = GetInt();
            BinaryReader br;
            int num = 0;
            for (int i = 0; i < N; i++)
            {
                br = new BinaryReader(File.OpenRead(GetString()));
                bw.Write(((int)(br.BaseStream.Length / sizeof(int))));
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    num = br.ReadInt32();
                    Show(num);
                    bw.Write(num);
                }
                br.Close();
                br.Dispose();
            }
            bw.Close();
            bw.Dispose();
        }
    }
}
