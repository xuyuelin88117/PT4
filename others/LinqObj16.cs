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
            Task("LinqObj16");
            var inFileName = GetString();
            var outFileName = GetString();

            var result = File.ReadLines(inFileName, Encoding.Default)
                .Select(x => x.Split()[0])
                .GroupBy(x => x, (k, v) => new
                {
                    year = k,
                    count = v.Count()
                })
                .OrderByDescending(x => x.count)
                .ThenBy(x => x.year)
                .Select(x => x.count + " " + x.year);

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
