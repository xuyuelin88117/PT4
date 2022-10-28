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
            Task("LinqObj96");

            var splA = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var splC = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var splD = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var splE = System.IO.File.ReadLines(GetString(), Encoding.Default);
            var fname = GetString();

            var arrA = splA.Select(s =>
            {
                var sp = s.Split(' ');
                return new { street=sp[0], year=int.Parse(sp[1]), code=int.Parse(sp[2])};
            });

            var arrC = splC.Select(s =>
            {
                var sp = s.Split(' ');
                return new { code = int.Parse(sp[0]), perc = int.Parse(sp[1]), name = sp[2]};
            });

            var arrD = splD.Select(s =>
            {
                var sp = s.Split(' ');
                return new {price = int.Parse(sp[0]), articul = sp[1], name =sp[2]};
            });

            var arrE = splE.Select(s =>
            {
                var sp = s.Split(' ');
                return new  { name = sp[0], articul = sp[1], code =int.Parse(sp[2])};
            });

            List<string> result = new List<string>();
            foreach (var y in arrA.OrderBy(x => x.year).GroupBy(x => x.year).ToArray())
            {
                var yearCodes = y.Select(x => x.code).ToArray();
                foreach (var shop in arrE.OrderBy(x => x.name).GroupBy(x => x.name).ToArray())
                {
                    var shopCodes = shop.Select(x => x.code).ToArray();
                    var codes = yearCodes.Intersect(shopCodes).ToArray();
                    List<int> codesTovs = new List<int>();
                    foreach (var code in codes)
                    {
                        var percent = arrC.Where(x => x.name == shop.First().name).
                                   Where(x => x.code == code).DefaultIfEmpty(new {code=0, perc=0, name = shop.First().name}).First().perc;

                        var shopCodeArticules = shop.Where(e => e.code == code).Select(e => e.articul);

                        var tovs = arrD.Where(x => x.name == shop.First().name).
                                    Where(x => shopCodeArticules.Contains(x.articul)).
                                    Select(x => (percent > 0 ? x.price - Math.Truncate(x.price * 0.01 * percent) : x.price) * 
                                    shop.Where(e => e.code == code).Where(e => e.articul == x.articul).ToArray().Count());

                        codesTovs.Add((int)tovs.Sum());
                    }
                    if (codesTovs.Sum() > 0)
                    {
                        result.Add(y.First().year + " " + shop.First().name + " " + codesTovs.Sum());
                    }

                }
            }
            File.WriteAllLines(fname, result.ToArray(), Encoding.Default);
        }
    }
}
