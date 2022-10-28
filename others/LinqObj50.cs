using System;
using PT4;
using System.Linq;
using System.IO;
using System.Text;

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
            Task("LinqObj50");
            var inFileName = GetString();
            var outFileName = GetString();

            var file = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    surname = x[0],
                    initials = x[1],
                    rating = Convert.ToInt32(x[2]) +
                             Convert.ToInt32(x[3]) +
                             Convert.ToInt32(x[4])
                });

            var result = file
                .GroupBy(x => 1)
                .Select(x =>
                {
                    var first = x.Select(y => y.rating)
                                 .Distinct()
                                 .OrderByDescending(y => y).Take(2);
                    var students = x.Where(y => first.Contains(y.rating));

                    return new [] { $"{first.First()} {first.Last()}"}
                        .Concat(students.Select(
                            y => $"{y.surname} {y.initials} {y.rating}"));
                })
                .Aggregate(Enumerable.Empty<string>(), (all, cur) =>
                    all.Concat(cur));

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
