// File: "LinqXml6"
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

        //给出了现有文本文件和要创建的 XML 文档的名称。文本文件的每一行都包含几个（一个或多个）整数，由一个空格分隔。
        //使用根元素 root、第一级元素行和第二级元素编号创建一个 XML 文档。
        //行元素对应源文件中的行，不包含子文本节点，每个行元素的数字元素包含对应行的一个数字（数字按降序排列）。
        //line 元素必须包含一个 sum 属性，该属性等于相应行中所有数字的总和。

        public static void Solve()
        {
            Task("LinqXml6");
            var A = File.ReadAllLines(GetString()).Select(x => x.Split());
            XDocument d = new XDocument(
                new XDeclaration("1.0", "us-ascii", "true"),
                new XElement("root", A.Select((e, i) => new XElement("line", new XAttribute("sum", e.Select(x => int.Parse(x)).Sum(x => x)), 
                e.Select(x => int.Parse(x)).OrderByDescending(x => x).Select((x, j) => new XElement("number", x)))))
                );
            d.Save(GetString());
        }
    }
}
