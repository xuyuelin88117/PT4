// File: "LinqObj20"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //��ʼ���а����й������˵���Ϣ�� ÿ������Ԫ�ذ��������ֶΣ�
    //<����> <ѧУ���> <��ѧ���>
    //ȷ����ЩѧУ������ݵ�������������࣬����ʾ��ЩѧУ�����������ݣ���ע��ѧУ��ţ�Ȼ��ע�������˵����ϣ���
    //ÿ�������˵���ϢӦ��ʾ�������ϣ�����ѧУ��ŵ��������򣬶�����ͬ�ı�� - ��ԭʼ���ݼ��е�������˳�����С�
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj20");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        name = s[0],
                        code = int.Parse(s[1]),
                    };
                }).OrderBy(e => e.code).ToArray();//GroupBy(e => e.code).
            var q = (from p in r group p by p.code into g orderby g.Count() descending select g).ToList();
            int a = q.First().Count();
            var c = (from p in q where p.Count() == a select p).ToList().Show();
            var loj = (from e in r
                       join b in c
                       on e.code equals b.Key
                       select new
                       {
                           e.code,
                           e.name,
                        }).Select(e => e.code + " " + e.name).ToList();
            File.WriteAllLines(GetString(), loj.ToArray(), Encoding.Default);
        }
    }
}
