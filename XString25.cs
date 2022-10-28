// File: "XString25"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("XString25");
            string s = GetString();
            long num = 0;
            bool canConvert = long.TryParse(s, out num);
            Show(num);
            Put(Convert.ToString(num,2));
            // byte[] byteData = Encoding.Unicode.GetBytes(s);
            // // StringBuilder strinBuilder = new StringBuilder(byteData.Length * 8);
            // StringBuilder strinBuilder = new StringBuilder(byteData.Length);
            // foreach (byte data in byteData)
            // {
            //     Show(data);
            //     //strinBuilder.Append(Convert.ToString(data, 2).PadLeft(8, '0'));
            //     strinBuilder.Append(Convert.ToString(data, 2));
            // }
            // Put(strinBuilder.ToString());
        }
    }
}
