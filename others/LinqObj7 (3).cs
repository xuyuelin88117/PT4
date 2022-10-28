// File: "LinqObj7"
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
        //<�γ̳���ʱ�䣨��СʱΪ��λ��> <���> <����> <�ͻ�����>
        //���ڴ���Ϊ K �Ŀͻ��������ĵ�ÿһ�꣬ȷ���ÿͻ������Ͽ�ʱ������·ݣ�����ж���������·ݣ���ѡ��������С���·ݣ���
        //������˳������������ʾÿ�����Ϣ���ꡢ���������µĿγ̳���ʱ�䡣
        //����ݱ�ŵĽ������Ϣ�����������û�й��ڴ��� K �Ŀͻ��˵����ݣ����ַ�����No data��д�����ļ���
        public static void Solve()
        {
            Task("LinqObj7");
            int K = GetInt();

            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        hours = int.Parse(s[0]),
                        year = int.Parse(s[1]),
                        month = int.Parse(s[2]),
                        code = int.Parse(s[3])
                    };
                }).Where(e => e.code == K).ToList()
                  .OrderByDescending(e => e.hours).ThenByDescending(e => e.year).ThenBy(e => e.month)
                  .ToList().GroupBy(e => new { e.year })
                  .Select(e => e.FirstOrDefault()).OrderByDescending(e => e.year)
                  .Select(e => e.year + " " + e.month + " " + e.hours);
            if(r.Count() != 0)
                File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
            else
            {
                string S = "No Data";
                File.WriteAllText(GetString(), S, Encoding.Default);
            }

        }

    }
}

