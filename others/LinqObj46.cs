using System;
using PT4;
using System.Linq;
using System.IO;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
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
            Task("LinqObj46");
            var inFileName = GetString();
            var outFileName = GetString();
            
            var result = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => new
                {
                    street = x.Split()[0],
                    gasoline = x.Split()[1],
                    company = x.Split()[3]
                })
                .GroupBy(x => x.company, (k, v) => new
                {
                    company = k,
                    stations = v.GroupBy(x => x.street, 
                        (k1, v1) => v1.Count()).Count(x => x == 3)
                })
                .OrderByDescending(x => x.stations)
                .ThenBy(x => x.company)
                .Select(x => x.stations + " " + x.company);

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
