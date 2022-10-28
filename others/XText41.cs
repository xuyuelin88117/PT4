// File: "XText41"
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
            Task("XText41");
            BinaryReader f1=new BinaryReader(File.OpenRead(GetString()));
            BinaryReader f2=new BinaryReader(File.OpenRead(GetString()));
            BinaryReader f3=new BinaryReader(File.OpenRead(GetString()));
            StreamWriter sw=new StreamWriter(File.Create(GetString()));

            int len = (int)(f1.BaseStream.Length)/4;
            
            int[] num1 = new int[len];int[] num2 = new int[len];int[] num3 = new int[len];
            for (int i = 0; i < len; i++){
                num1[i] = f1.ReadInt32();num2[i] = f2.ReadInt32();num3[i] = f3.ReadInt32();
            }
            Show(num1);
            for(int i = 0;i<len ;i++){
                string s1 = num1[i].ToString();string s2 = num2[i].ToString();string s3 = num3[i].ToString();
                int l1 = s1.Length;int l2 = s2.Length;int l3 = s3.Length;
                s1 = "|" + s1 + new string(' ',20-l1) + s2 + new string(' ',20-l2 )+ s3 + new string(' ',20-l3) + "|";
                //Show(s1.Length);
                sw.Write(s1+"\n");
            }


            f1.Close();f2.Close();f3.Close();sw.Close();
        }
    }
}
