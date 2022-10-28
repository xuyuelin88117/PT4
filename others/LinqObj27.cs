// File: "LinqObj27"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //��ʼ���а����йؾ�ס�� 144 �乫Ԣ�� 9 �㽨���е�ˮ���ծ���˵���Ϣ��ÿ������Ԫ�ذ��������ֶΣ�
    //<����> <��Ԣ����> <ծ��>
    //ծ����С����ʾ��ȫ��Ϊ¬����С������Ϊ��ȣ���ÿ��¥���ÿ����ڶ��� 4 �׹�Ԣ��
    //���ڷ��ӵ� 9 �㣬ÿ�㶼��ʾ��ס�ڸ�¥���ծ���˵���Ϣ��ծ����������¥��������¥��������ծ������������������ʾ����
    //�ڵ�����������ʾ�й�ÿ��¥�����Ϣ������ծ�����������������򣬶���ƥ������� - ��¥�����������
    //����κ�¥�㶼û��ծ���ˣ���Ҫ��ʾ�йظ�¥������ݡ�
    //һ��4����Ԣ��ÿ����Ԣ9�㣬ÿ����Ԣÿ��4�����䣬��1�Ź�Ԣ��ʼ��ţ����Ź�Ԣһ¥��һ������37�š�
    //��ȷ����Ԣ���ĸ�����ȷ������
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj27");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    int c = int.Parse(s[1]) % 36;
                    int d = 0;
                    if (c == 0)
                        d = 9;
                    else if (c % 4 == 0)
                        d = c / 4;
                    else if (c % 4 != 0)
                        d = c / 4 + 1;
                    return new
                    {
                        name = s[0],
                        code = d,
                        money = float.Parse(s[2]),
                    };
                }).GroupBy(e => e.code,(k, ee) => new { k, sum = ee.Sum(c => c.money),count = ee.Count()})
                  .OrderBy(e => e.count).ThenBy(e => e.k).Select(e => e.count.ToString() + " " + e.k + " " + e.sum.ToString("#0.00"));
            File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
        }
    }
}
