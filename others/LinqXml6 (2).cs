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

        //�����������ı��ļ���Ҫ������ XML �ĵ������ơ��ı��ļ���ÿһ�ж�����������һ����������������һ���ո�ָ���
        //ʹ�ø�Ԫ�� root����һ��Ԫ���к͵ڶ���Ԫ�ر�Ŵ���һ�� XML �ĵ���
        //��Ԫ�ض�ӦԴ�ļ��е��У����������ı��ڵ㣬ÿ����Ԫ�ص�����Ԫ�ذ�����Ӧ�е�һ�����֣����ְ��������У���
        //line Ԫ�ر������һ�� sum ���ԣ������Ե�����Ӧ�����������ֵ��ܺ͡�

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
