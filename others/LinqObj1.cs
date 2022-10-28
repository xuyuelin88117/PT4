using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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

        class Client
        {
            public int code { get; set; }
            public int year { get; set; }
            public int month { get; set; }
            public int duration { get; set; }

            public Client(int c, int y, int m, int d)
            {
                code = c;
                year = y;
                month = m;
                duration = d;
            }

            public override string ToString() => String.Format("{0} {1} {2}", duration, year, month);
        }

        public static void Solve()
        {
            Task("LinqObj1");
            var spl = System.IO.File.ReadAllLines(GetString(), Encoding.Default);
            System.IO.File.WriteAllText(GetString(), spl.Select(s =>
            {
                var sp = s.Split(' ');
                return new Client(int.Parse(sp[0]), int.Parse(sp[1]), int.Parse(sp[2]), int.Parse(sp[3]));
            }).OrderByDescending(x => x.duration).Last().ToString());
        }
    }
}
