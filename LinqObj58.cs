// File: "LinqObj58"
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
        public static void Solve()
        {
            Task("LinqObj58");
            string f = GetString();
            string s = GetString();
            var d = XDocument.Load(f);
            d.Root.Add(new XAttribute(XNamespace.Xmlns + "node", s));
            foreach (var e in d.Root.Elements())
            {
                e.Add(new XAttribute((XNamespace)s + "count", e.DescendantNodes().Count()),
                      new XAttribute(XNamespace.Xml + "count", e.Descendants().Count()));
            }
            // d.Save(f);
        }
    }
}
