// File: "LinqXml38"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        static int count(XElement x)
        {
            int k = 0;
            while (x != null)
            {
                x = x.Parent;
                k++;
            }
            return k;
        }
        public static void Solve()
        {
            Task("LinqXml38");
            Task("LinqXml38");
            string name = GetString();
            XDocument d = XDocument.Load(name);
            var a = d.Root.Descendants().OrderBy(x=> count(x));
            string add = "";
           var y = a.First();
            y.Name = y.Name;
            XElement cur;
            foreach (var x in a)
            {
                cur = x.Parent;
                add = "";
                while (cur!=null)
                {
                    add=cur.Name+"-"+add;
                    cur = cur.Parent;

                }
                x.Name = add+x.Name;
            }

            d.Save(name);
        }
    }
}
