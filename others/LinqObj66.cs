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
            Task("LinqObj66");
            var s = GetString();
            var inFileName = GetString();
            var outFileName = GetString();

            var result = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    subject = x[0],
                    surname = x[1],
                    initials = x[2],
                    mark = Convert.ToInt32(x[3]),
                    grade = Convert.ToInt32(x[4])
                })
                .GroupBy(x => x.grade, (k, v) => new
                {
                    grade = k,
                    students = v
                        .Where(x => x.subject == s)
                        .GroupBy(y => $"{y.surname} {y.initials}",
                        (k1, v1) => new
                        {
                            name = k1,
                            hasBadMark = v1.Any(z => z.mark == 2),
                            avgMark = v1.Average(z => z.mark)
                        })
                        .Count(y => y.avgMark >= 3.5 && !y.hasBadMark)
                })
                .OrderBy(x => x.grade)
                .Select(x => $"{x.grade} {x.students}"); 

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
