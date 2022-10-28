using System;
using PT4;
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
            Task("LinqObj93");
            var aInFileName = GetString();
            var bInFileName = GetString();
            var cInFileName = GetString();
            var eInFileName = GetString();
            var outFileName = GetString();

            var a = File.ReadLines(aInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    code = x[0],
                    street = x[1],
                    year = x[2]
                });

            var b = File.ReadLines(bInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    category = x[0],
                    country = x[1],
                    article = x[2]
                });

            var c = File.ReadLines(cInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    shop = x[0],
                    code = x[1],
                    discount = Convert.ToInt32(x[2])
                });

            var e = File.ReadLines(eInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    code = x[0],
                    article = x[1],
                    shop = x[2]
                });

            var result = e.Join(b, x => x.article, x => x.article,
                (x, y) => new
                {
                    x.code,
                    x.shop,
                    y.country
                })
                .Join(a, x => x.code, x => x.code, (x, y) => new
                {
                    x.code,
                    x.shop,
                    x.country,
                    y.street
                })
                .GroupJoin(c, x => x.code + x.shop, x => x.code + x.shop,
                (x, y) => new
                {
                    x.country,
                    x.street,
                    discount = y.Select(z => z.discount).FirstOrDefault() //y.discount
                })
                .OrderBy(x => x.country)
                .ThenBy(x => x.street)
                .GroupBy(x => $"{x.country} {x.street}", (k, v) => new
                {
                    place = k,
                    maxDiscount = v.Max(y => y.discount)
                })
                .Select(x => $"{x.place} {x.maxDiscount}");

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
