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
            Task("LinqObj26");
            var spl = System.IO.File.ReadAllLines(GetString(), Encoding.Default);
            var fname = GetString();

            var arr = spl.Select(s =>
            {
                var sp = s.Split(' ');
                return new { flat = int.Parse(sp[0]), name = sp[1], debt = double.Parse(sp[2], System.Globalization.CultureInfo.InvariantCulture)};
            }).ToArray();

            var result = arr.Where(x => x.debt > 0).OrderBy(x => (x.flat - 1) / 36 + 1).GroupBy(x => (x.flat - 1) / 36 + 1).Select(x =>
            {
                var porch = (x.First().flat - 1) / 36 + 1;
                var dbt = Math.Round(x.Select(e => e.debt).Average(), 2);
                return String.Format("{0} {1} {2}", porch, x.Count(), dbt.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture));
            }).ToArray();

            File.WriteAllLines(fname, result.ToArray(), Encoding.Default);

        }
    }
}
