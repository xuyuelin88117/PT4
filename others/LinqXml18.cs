using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Remoting.Messaging;
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
            Task("LinqXml18");
            string name = GetString();
            XDocument d = XDocument.Load(name);
            var arr = d.Descendants().Attributes().Distinct().GroupBy(x=>x.Name).Select(x =>
            {
                var n = x.First().Name;
                var v = x.OrderBy(y=>y.Value).Select(y => y.Value);
                return new {name= n,values = v};
            });


            foreach (var elem in arr)
            {
                Put(elem.name.ToString());
                foreach (var x in elem.values)
                    Put(x.ToString());
            }

            }
    }
}
