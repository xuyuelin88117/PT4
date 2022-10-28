using PT4;
using System;
using System.Linq;

namespace PT4Tasks
{
    public class MyTask: PT
    {
        // ��� ������� ����� ������ LinqBegin �������� ���������
        // �������������� ������, ������������ � ���������:
        //
        //   GetEnumerableInt() - ���� �������� ������������������;
        //
        //   GetEnumerableString() - ���� ��������� ������������������;
        //
        //   Put() (����� ����������) - ����� ������������������;
        //
        //   Show() � Show(cmt) (������ ����������) - ���������� ������
        //     ������������������, cmt - ��������� �����������;
        //
        //   Show(e => r) � Show(cmt, e => r) (������ ����������) -
        //     ���������� ������ �������� r, ���������� �� ��������� e
        //     ������������������, cmt - ��������� �����������.

        public static void Solve()
        {
            Task("LinqBegin55");
            var a = GetEnumerableInt();
            var b = GetEnumerableInt();

            var result = a.GroupJoin(b, x => x.ToString().Last(),
                x => x.ToString().Last(), 
                (xa, xb) => xb.DefaultIfEmpty(0)
                .Select(x => xa + ":" + x)).SelectMany(x => x)
                .OrderByDescending(x => Convert.ToInt32(x.Split(':').First()))
                .ThenBy(x => Convert.ToInt32(x.Split(':').Last()));
            result.Show().Put();
        }
    }
}
