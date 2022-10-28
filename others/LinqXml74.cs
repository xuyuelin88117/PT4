// File: "LinqXml74"
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
            Task("LinqXml74");
            string fname = GetString();
            var d = XDocument.Load(fname);
            var n = d.Root.Name.Namespace;
            var da = d.Root.Descendants(n + "company").Select(e =>
            {
                var sp = e.Attribute("station").Value.Split('.');
                return new
                {
                    num = e.Parent.Name.LocalName,
                    street = sp[0] + ".",
                    brand = sp[1].Substring(1),
                    price = int.Parse(e.Attribute("price").Value),
                };
            });
            var nt = da.GroupBy(y => y.street).Select( e =>e.Key).Distinct().OrderBy( e => e);
            d.Root.ReplaceNodes(
                da.GroupBy(y => y.brand, (e,ee)=> new XElement(n + e, 
                nt.GroupJoin(ee, e1 => e1, e2 => e2.ToString(), (ee1, ee2) => new XElement(
                    n + ee1, 
                    new XAttribute("brand92", da.Where(x => x.street.Equals(ee1) && x.brand.Equals(e) && x.num.Equals("brand92")).Select(x => x.price).DefaultIfEmpty(0).FirstOrDefault()), 
                    new XAttribute("brand95", da.Where(x => x.street.Equals(ee1) && x.brand.Equals(e) && x.num.Equals("brand95")).Select(x => x.price).DefaultIfEmpty(0).FirstOrDefault()), 
                    new XAttribute("brand98", da.Where(x => x.street.Equals(ee1) && x.brand.Equals(e) && x.num.Equals("brand98")).Select(x => x.price).DefaultIfEmpty(0).FirstOrDefault())))))
                .OrderBy( y => y.Name.LocalName)
                );
            d.Save(fname);
        }
    }
}
