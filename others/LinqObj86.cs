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
            Task("LinqObj86");
            var cInFileName = GetString();
            var dInFileName = GetString();
            var eInFileName = GetString();
            var outFileName = GetString();

            var c = File.ReadLines(cInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    discount = Convert.ToInt32(x[0]),
                    code = x[1],
                    shop = x[2]
                });

            var d = File.ReadLines(dInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    shop = x[0],
                    price = Convert.ToInt32(x[1]),
                    article = x[2]
                });

            var e = File.ReadLines(eInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    code = x[0],
                    shop = x[1],
                    article = x[2]
                });

            var result = e.Join(d, x => x.shop + x.article, 
                x => x.shop + x.article,
                (x, y) => new
                {
                    x.shop,
                    x.article,
                    x.code,
                    y.price
                })
                .GroupJoin(c, x => x.code + x.shop, x => x.code + x.shop,
                (x, y) => new
                {
                    x.shop,
                    x.article,
                    discount = y.Select(z => z.discount).FirstOrDefault() 
                        * x.price / 100
                })
                .OrderBy(x => x.article)
                .ThenBy(x => x.shop)
                .GroupBy(x => $"{x.article} {x.shop}", (k, v) => new
                {
                    item = k,
                    maxDiscount = v.Max(y => y.discount)
                })
                .Select(x => $"{x.item} {x.maxDiscount}");

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
