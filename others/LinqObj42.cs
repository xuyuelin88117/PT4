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
            Task("LinqObj42");

            var spl = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var fname = GetString();
            var arr = spl.Select(s =>
            {
                var sp = s.Split(' ');
                return new { petrol = int.Parse(sp[0]), name = sp[1], street = sp[2], price =int.Parse(sp[3])};
            });

            var result = arr.OrderBy(x => x.street).GroupBy(x => x.street).Select(x =>
            {
                var str = x.First().street;
                var price92 = x.Where(cur => cur.petrol == 92).Select(cur => cur.price).DefaultIfEmpty(0).Min();
                var price95 = x.Where(cur => cur.petrol == 95).Select(cur => cur.price).DefaultIfEmpty(0).Min();
                var price98 = x.Where(cur => cur.petrol == 98).Select(cur => cur.price).DefaultIfEmpty(0).Min();

                return String.Format("{0} {1} {2} {3}", str, price92, price95, price98);
            });
            File.WriteAllLines(fname, result, Encoding.Default);
        }
    }
}
