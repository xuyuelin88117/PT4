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
            Task("LinqObj64");

            var spl = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var fname = GetString();
            var arr = spl.Select(s =>
            {
                var sp = s.Split(' ');
                return new { level = int.Parse(sp[0]), name = sp[1]+" "+sp[2], subject = sp[3], mark = int.Parse(sp[4])};
            });

            var result = arr.Where(e => e.subject == "�����������").OrderBy(x => x.level).ThenBy(x => x.name).
                GroupBy(x => x.name).Where(x => x.Average(e => e.mark) >= 4.00).Select(x =>
                {
                    return String.Format("{0} {1} {2}", x.First().level, x.First().name,
                        (Math.Round(x.Average(e => e.mark) / 1.00, 2))
                        .ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
                }).DefaultIfEmpty("��������� �������� �� �������");

            File.WriteAllLines(fname, result, Encoding.Default);

        }
    }
}
