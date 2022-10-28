// File: "LinqObj48"
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
        //Դ���а����йؼ���վ������վ������Ϣ��ÿ������Ԫ�ذ��������ֶΣ�
        //<1 ���۸񣨸�ȣ�> <�ֵ�> <���͵ȼ�> <��˾>
        //��˾���ƺͽֵ����Ʋ������ո� 92��95��98��Ϊ�����ƺţ�ÿ�ҹ�˾��ÿ���ֵ��ϲ�����һ������վ��
        //ͬһ�ҹ�˾�Ĳ�ͬ����վ�ļ۸���ܻ�������ͬ��ö��ԭʼ���ݼ��а����Ľֵ��͹�˾�����п�����ϣ�
        //��Ϊÿ�ԡ�street-company������ֵ����ơ���˾���ƺ͸ù�˾λ�ڸýֵ��ļ���վ�ṩ������Ʒ���������û�м���վ����ٶ�����Ϊ 0����
        //����������ʾ�й�ÿ�Ե���Ϣ��������ĸ˳�򰴽ֵ��������򣬶�����ͬ�Ľֵ����� - ����˾���ƣ�Ҳ����ĸ˳������
        public static void Solve()
        {
            Task("LinqObj48");
            var all = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        sc = s[1] + " " + s[3],
                        street = s[1],
                        company = s[3],
                        number = int.Parse(s[2]),
                    };
                }).OrderBy(e => e.street).ThenBy(e => e.company).ToArray();
            var a = all.GroupBy(e => e.sc, (key, ee) => new { key, count = ee.Count() }).ToArray();
            var street = all.Select(e => e.street).GroupBy(e => e).Select(e => e.FirstOrDefault()).OrderBy(e => e).ToArray();
            var company = all.Select(e => e.company).GroupBy(e => e).Select(e => e.FirstOrDefault()).OrderBy(e => e).ToArray();
            var allkinds = street.SelectMany(e => company.Select(b => e + " " + b)).Select(e => new { sc = e, count = 0 }).ToArray();
            var inner = (from u in allkinds
                         join g in a
                         on u.sc equals g.key into G
                         from aa in G.DefaultIfEmpty()
                         select new { name = u.sc, count = (aa == null) ? 0 : aa.count })
                      .Select(e => e.name + " " + e.count.ToString());
            File.WriteAllLines(GetString(), inner.ToArray(), Encoding.Default);
        }
    }
}
