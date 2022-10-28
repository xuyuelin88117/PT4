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
        // 解决LinqXml组任务时，如下
         // 任务簿中定义的其他方法可用：
         // (*) Show() 和 Show(cmt)（扩展方法） - 调试输出
         // 一个序列，cmt - 字符串注释；
         // (*) Show(e => r) 和 Show(cmt, e => r) (扩展方法) -
         // r 值的调试输出，从元素 e 获得
         // 一个序列，cmt - 字符串注释。

        public static void Solve()
        {
            Task("LinqXml7");
            var a = File.ReadAllLines(GetString());
            var b = a.Select(e=>e.Split(" ").Select(e3=>int.Parse(e3)).Reverse()  ).Show("A:");
            //var c = a.Select(e=>e.Split(" ")).Show();

            XDocument d = new XDocument(
                new XDeclaration(null, "us-ascii", null),
                new XElement("root",
                b.Select(e =>new XElement("line",new XElement("sum-positive",e.Where(e2=>e2>0).Sum()),e.Where(e =>e<0).Select(e => new XElement("number-negative", e))/* new XElement("number-negative",e.Where(e2=>e2<0). Select(e3=>e3)) */))));
            d.Save(GetString());


        }
    }
}
