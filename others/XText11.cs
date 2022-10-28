// File: "XText11"
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
        public static async void Solve()
        {
            Task("XText11");
            string f = GetString(),f2 = "dd";
            StreamReader sr = new StreamReader(File.OpenRead(f));
            StreamWriter sw = new StreamWriter(File.Create(f2));

            while (!sr.EndOfStream){
                string str = sr.ReadLine();
                if(str!=""){
                    sw.Write(str+"\n");
                }else{
                    sw.Write(""+"\n");sw.Write(""+"\n");
                }

            }
            sr.Close();sw.Close();
            File.Delete(f);
            File.Move(f2,f);

        }
    }
}
