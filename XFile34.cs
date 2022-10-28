// File: "XFile34"
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
            Task("XFile34");
            string s1 = GetString(), s2 = "$F34$.tmp";
            FileStream f = new FileStream(s1, FileMode.Open);
            FileStream f1 = new FileStream(s2, FileMode.Create);

            BinaryReader br = new BinaryReader(f);
            BinaryWriter b1 = new BinaryWriter(f1);
            for(int i = 0; i < br.BaseStream.Length; i += sizeof(int)){
                int num = br.ReadInt32();
                Show(num);
                if(num >= 0){
                    b1.Write(num);
                }    
            }

            br.Close();
            b1.Close();
            System.IO.File.Delete(s1);
            File.Copy(s2,s1);
        }
    }
}
