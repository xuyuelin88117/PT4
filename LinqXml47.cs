// File: "LinqXml47"
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
            Task("LinqXml47");
            string fileName = GetString();
            var doc = XDocument.Load(fileName);

            foreach (var e in doc.Descendants().Where(el => el.Descendants().Count() > 0))
            {
                e.FirstNode.AddAfterSelf(new XElement("has-comments", e.DescendantNodes()
                                                                        .OfType<XComment>().Count() > 0));
            }
            doc.Save(fileName);
        }
    }
}
