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
            Task("LinqObj73");
            var fstInFileName = GetString();
            var sndInFileName = GetString();
            var outFileName = GetString();

            var a = File.ReadLines(fstInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    year = x[0],
                    code = x[1],
                    street = x[2]
                });

            var c = File.ReadLines(sndInFileName, Encoding.Default)
                .Select(x => x.Split())
                .Select(x => new
                {
                    code = x[0],
                    shop = x[1],
                    discount = x[2]
                });

            var result = a.Join(c, x => x.code, x => x.code, (x, y) => new
            {
                y.shop,
                x.street
            })
            .OrderBy(x => x.shop)
            .ThenBy(x => x.street)
            .GroupBy(x => $"{x.shop} {x.street}", (k, v) => new
            {
                shop = k,
                count = v.Count()
            })
            .Select(x => $"{x.shop} {x.count}")
            .Show();

            File.WriteAllLines(outFileName, result, Encoding.Default);
        }
    }
}
