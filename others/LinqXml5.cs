using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    using System.Xml;

    public class MyTask: PT
    {
        // ��� ������� ����� ������ LinqXml �������� ���������
        // �������������� ������ ����������, ������������ � ���������:
        //
        //   Show() � Show(cmt) - ���������� ������ ������������������,
        //     cmt - ��������� �����������;
        //
        //   Show(e => r) � Show(cmt, e => r) - ���������� ������
        //     �������� r, ���������� �� ��������� e ������������������,
        //     cmt - ��������� �����������.

        public static void Solve()
        {
            Task("LinqXml5");
            var a = File.ReadAllLines(GetString(), Encoding.Default);
            XDocument d = new XDocument(
                new XDeclaration(null, "windows-1251", null),
                new XElement("root",
                    a.Select((e, lines) => new XElement("line", new XAttribute("num", lines + 1),
                        e.Split(' ').Select((x, words) => new XElement("word", x, new XAttribute("num", words + 1)))))));
            d.Save(GetString());


        }
    }
}
