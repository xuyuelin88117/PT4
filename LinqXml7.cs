// File: "LinqXml7"
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
        // When solving tasks of the LinqXml group, the following
        // additional methods defined in the taskbook are available:
        // (*) Show() and Show(cmt) (extension methods) - debug output
        //       of a sequence, cmt - string comment;
        // (*) Show(e => r) and Show(cmt, e => r) (extension methods) -
        //       debug output of r values, obtained from elements e
        //       of a sequence, cmt - string comment.

        public static void Solve()
        {
            Task("LinqXml7");
            var file = File.ReadAllLines(GetString());
            var felem = file.Select(e => e.Split(" ")
                                   .Select(e3 => int.Parse(e3))
                                   .Reverse())
                     .Show("A:");
            XDocument doc = new XDocument(
                 new XDeclaration(null, "us-ascii", null),
                    new XElement("root",felem.Select(e => 
                        new XElement("line", 
                            new XElement("sum-positive", 
                                e.Where(e2 => e2 > 0).Sum()),
                                e.Where(e => e < 0)
                                 .Select(e => new XElement("number-negative", e))))));
            doc.Save(GetString());
        }

    }
}
