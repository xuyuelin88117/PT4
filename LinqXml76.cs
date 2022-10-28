// File: "LinqXml76"
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
            Task("LinqXml76");
            string fileName = GetString();
            var doc = XDocument.Load(fileName);
            var ns = doc.Root.Name.Namespace;
            doc.Root.ReplaceNodes(doc.Root.Elements().Select(e => new XElement(ns + "debt",
                                                                               new XAttribute("house", e.Element(ns + "house").Value),
                                                                               new XAttribute("flat", e.Element(ns + "flat").Value),
                                                                               new XElement(ns + "name", e.Element(ns + "name").Value),
                                                                               new XElement(ns + "value", e.Element(ns + "debt").Value))));
            doc.Save(fileName);
        }
    }
}
