using System;
using System.IO;
using PT4;
using System.Linq;
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
            Task("LinqObj11");
            var inFileName = GetString();
            var outFileName = GetString();

            var result = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => new
                {
                    data = x.Split()[1] + " " + x.Split()[2],
                    duration = Convert.ToInt32(x.Split()[3])
                })
                .GroupBy(x => x.data, (k, v) => new
                {
                    data = k,
                    sumDuration = v.Sum(x => x.duration)
                })
                .OrderBy(x => x.sumDuration)
                .ThenByDescending(x => Convert.ToInt32(x.data.Split(' ').First()))
                .ThenBy(x => Convert.ToInt32(x.data.Split(' ').Last()))
                .Select(x => x.sumDuration + " " + x.data);
            
            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
