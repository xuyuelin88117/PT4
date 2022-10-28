using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    public class MyTask: PT
    {
        // ��� ������ ������ ����� �� ��������� ���������� �����
        // � ������ a ���� string[] ����������� ��������:
        //
        //   a = File.ReadAllLines(GetString(), Encoding.Default);
        //
        // ��� ������ ������������������ s ���� IEnumerable<string>
        // � �������������� ��������� ���� ����������� ��������:
        //
        //   File.WriteAllLines(GetString(), s.ToArray(), Encoding.Default);
        //
        // ��� ������� ����� ������ LinqObj �������� ���������
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
            Task("LinqObj80");

            var splD = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var splE = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var fname = GetString();

            var arrD = splD.Select(s =>
            {
                var sp = s.Split(' ');
                return new { price = int.Parse(sp[0]), name = sp[1], articul = sp[2]};
            });

            var arrE = splE.Select(s =>
            {
                var sp = s.Split(' ');
                return new { articul = sp[0], name = sp[1], code = int.Parse(sp[2])};
            });

            var result = arrD.OrderBy(x => x.name).ThenBy(x => x.articul).Select(x =>
            {
                return String.Format("{0} {1} {2}", x.name, x.articul, x.price * arrE.Where(e => e.name == x.name).Count(e => e.articul == x.articul));
            });

            File.WriteAllLines(fname, result, Encoding.Default);

        }
    }
}
