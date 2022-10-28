using System;
using System.Threading;
using System.Globalization;
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
            Task("LinqObj36");
            var inFileName = GetString();
            var outFileName = GetString();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var result = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => new
                {
                    apartment = Convert.ToInt32(x.Split()[0]),
                    building = Math.Ceiling(Convert.ToInt32(x.Split()[0])/36.0),
                    name = x.Split()[1],
                    debt = Convert.ToDouble(x.Split()[2])
                })
                .GroupBy(
                    x => Math.Ceiling((x.apartment - 36*(x.building - 1))/4.0),
                    (k, v) => 
                    {
                        var avg = v.Average(y => y.debt);
                        return new
                        {
                            floor = k,
                            owners = v.Where(x => x.debt <= avg)
                                      .OrderBy(x => x.debt)
                        };
                    })
                .OrderBy(x => x.floor)
                .Select(x => x.owners.Aggregate("", (all, cur) => all 
                    + (all == "" ? "" : "\n")
                    + x.floor + " " + $"{cur.debt:0.00}" + " " + cur.name +  " "
                    + cur.apartment));

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
