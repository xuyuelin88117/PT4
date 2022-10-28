using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
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

        static int count_parrent(XElement x)
        {
            int k = 0;
            while (x != null)
            {
                x = x.Parent;
                k++;
            }
            return k;
        }
        
        public static void Solve()
        {
            Task("LinqXml38");
            string name = GetString();
            XDocument d = XDocument.Load(name);
            var a = d.Root.Descendants().OrderBy(x=> count_parrent(x));
            string add = "";
           var y = a.First();
            y.Name = y.Name;
            XElement cur;
            foreach (var x in a)
            {
                cur = x.Parent;
                add = "";
                while (cur!=null)
                {
                    add=cur.Name+"-"+add;
                    cur = cur.Parent;

                }
                x.Name = add+x.Name;
            }

            d.Save(name);
        }
    }
}
