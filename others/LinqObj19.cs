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
            Task("LinqObj19");
            var spl = System.IO.File.ReadAllLines(GetString(), Encoding.Default);
            var filename = GetString();

            var arr = spl.Select(s =>
            {
                var sp = s.Split(' ');
                return new { name = sp[0], year = sp[1], school = int.Parse(sp[2])};
            });

            var result = arr.OrderBy(x=>x.school).GroupBy(x => x.school).Select(x =>
            {
                var e = x.First();
                return e.school + " " + x.Count() + " " + e.name;
            }).ToArray();

            File.WriteAllLines(filename, result.ToArray(), Encoding.Default);
        }
    }
}
