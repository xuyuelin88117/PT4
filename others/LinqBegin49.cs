using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            Task("LinqBegin49");
            var A = GetEnumerableString();
            var B = GetEnumerableString().OrderBy(x=>x);
            var C = GetEnumerableString().OrderByDescending(x=>x);
            A.Join(B, x => x[0], y => y[0], (x, y) => x + "=" + y).Join(C, x=>x[0],y=>y[0],(x,y)=>x+"="+y).Put();
        }
    }
}
