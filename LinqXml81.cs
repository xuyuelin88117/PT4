// File: "LinqXml81"
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
            Task("LinqXml81");
            string fileName = GetString();
            var doc = XDocument.Load(fileName);
            var ns = doc.Root.Name.Namespace;
            var data = doc.Root.Elements().Descendants().Select(el => new { 
                                                                                house = int.Parse(el.Parent.Name.LocalName.Replace("house","")),
                                                                                flat = int.Parse(el.Name.LocalName.Replace("flat","")),
                                                                                entrance = (int.Parse(el.Name.LocalName.Replace("flat","")) - 1) / 36 + 1,
                                                                                name = el.Attribute("name").Value,
                                                                                debt = (double)el.Attribute("debt")
                                                                           })
                                .OrderBy(h => h.house).ThenBy(e => e.entrance).ThenBy(f => f.flat);
            doc.Root.ReplaceNodes(data.GroupBy(h => h.house,
                                       (hn, ee) => new XElement(ns + "house",
                                                    new XAttribute("number", hn),
                                                    ee.GroupBy(en => en.entrance,
                                                       (e, el) => new XElement(ns + "entrance",
                                                                   new XAttribute("number", e),
                                                                   new XAttribute("count", el.Select(c => c.name).Count()),
                                                                   new XAttribute("avr-debt", Math.Truncate(el.Select(d => d.debt).Sum() / el.Select(c => c.name).Count())),
                                                                   el.Select(df => new XElement(ns + "debt",
                                                                                        new XAttribute("flat", df.flat),
                                                                                        new XAttribute("name", df.name),
                                                                                        df.debt))
                                                                     .Where(ad => (double)ad >= Math.Truncate(el.Select(d => d.debt).Sum() / el.Select(c => c.name).Count())))))));
            doc.Save(fileName);

        }
    }
}
