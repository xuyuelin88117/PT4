// File: "XFile13"
using PT4;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static async void Solve()
        {
            Task("XFile13");
            FileStream f = new FileStream(GetString(), FileMode.Open, FileAccess.ReadWrite);
            FileStream f1 = new FileStream(GetString(), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            FileStream f2 = new FileStream(GetString(), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            for (int i = 1; i <= f.Length / 4; i++)
            {
                byte[] b = new Byte[4];
                f.Seek(-4 * i, SeekOrigin.End);
                f.Read(b, 0, 4);
                if (System.BitConverter.ToInt16(b, 0) > 0)
                    f1.Write(b, 0, 4);
                else
                    f2.Write(b, 0, 4);

            }
            f1.Close();
            f2.Close();

        }
    }
}
