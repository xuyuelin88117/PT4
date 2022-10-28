// File: "LinqXml86"
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
            Task("LinqXml86");
            string fileName = GetString();
            var doc = XDocument.Load(fileName);
            var ns = doc.Root.Name.Namespace;
            var data = doc.Root.Elements().Select(el => new { 
                                                                name = el.Attribute("name").Value.Replace(" ","_"),
                                                                cl = int.Parse(el.Attribute("class").Value),
                                                                mark = el.Element(ns + "info").Attribute("mark").Value,
                                                                subject = el.Element(ns + "info").Attribute("subject").Value
                                                             })
                                .OrderBy(el => el.name).ThenByDescending(el => el.mark).ThenBy(el => el.subject);
            doc.Root.ReplaceNodes(data.GroupBy(n => n.name +" " + n.cl,
                                               (n, el) => new XElement(ns + n.Split(' ')[0],
                                                                       new XAttribute("class", n.Split(' ')[1]),
                                                                       el.Select(e => new XElement(ns + "mark" + e.mark,
                                                                                                   new XAttribute("subject", e.subject))))));
            doc.Save(fileName);
        }
    }
}
