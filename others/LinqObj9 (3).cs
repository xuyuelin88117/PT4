// File: "LinqObj9"
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
        // To read strings from the source text file into
        // a string sequence (or array) s, use the statement:
        //   s = File.ReadLines(GetString());
        // To write the sequence s of IEnumerable<string> type
        // into the resulting text file, use the statement:
        //   File.WriteAllLines(GetString(), s);
        // When solving tasks of the LinqObj group, the following
        // additional methods defined in the taskbook are available:
        // (*) Show() and Show(cmt) (extension methods) - debug output
        //       of a sequence, cmt - string comment;
        // (*) Show(e => r) and Show(cmt, e => r) (extension methods) -
        //       debug output of r values, obtained from elements e
        //       of a sequence, cmt - string comment.

        //����һ������ K�����������ĵ�һλ�ͻ��Ĵ��롣Դ���а����йظý������Ŀͻ�����Ϣ��ÿ������Ԫ�ذ������������ֶΣ�
        //<�ͻ�����> <�γ̳���ʱ�䣨��СʱΪ��λ��> <����> <��>
        //���ڴ���Ϊ K �Ŀͻ��������ĵ�ÿһ�꣬ȷ���ÿͻ��Ŀγ̳���ʱ�䳬�� 15 Сʱ�����������ȴ�ӡ������Ȼ���ӡ��ݣ���
        //���ĳ��û����Ҫ���·ݣ������0��������ʾÿһ�����Ϣ����������������ݽ����������������ȣ�����������
        //���û�й���ָ���ͻ��˵����ݣ����ַ�����No data��д�����ļ���
        public static void Solve()
        {
            Task("LinqObj9");
            int K = GetInt();
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        hours = int.Parse(s[1]),
                        year = int.Parse(s[3]),
                        month = int.Parse(s[2]),
                        code = int.Parse(s[0])
                    };
                }).Where(e => e.code == K).ToList();

            var y = (from p in r
                     group p by p.year
                     into g
                     select new { Year = g.Key, count = 0 }).OrderBy(e => e.Year).ToList();

            var d = (from p in r.Where(e => e.hours > 15).ToList()
                     group p by p.year
                      into g
                     select new { Year = g.Key, count = g.Count() }).ToList();

            var inner = (from g in y
                        join u in d
                        on g.Year equals u.Year into G
                        from a in G.DefaultIfEmpty()
                        select new { Year = g.Year, Count = (a == null)? 0 : a.count})
                      .OrderByDescending(e => e.Count).ThenBy(e => e.Year).Select(e => e.Count + " " + e.Year).Show();

            if (r.Count() != 0)
                File.WriteAllLines(GetString(), inner.ToArray(), Encoding.Default);
            else
            {
                string S = "No data";
                File.WriteAllText(GetString(), S, Encoding.Default);
            }

        }
    }
}
