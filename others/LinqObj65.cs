// File: "LinqObj65"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //����һ���ַ��� S����������Ŀ֮һ�����ƣ����������λ�����Դ���а����й���������Ŀ��ѧ���ɼ�����Ϣ��
    //�����е�ÿ��Ԫ�ض������й�һ�����������ݣ������������ֶΣ�
    //<����> <������д> <��Ŀ����> <�ȼ�> <�༶>
    //ѧ��֮��û��������ͬ�������Ϻ�����ĸ��ͬ����
    //�༶ָ��Ϊ������������ 2-5 ��Χ�ڵ���������Ŀ�����ô�д��ĸ��ʾ��
    //���ڳ�ʼ���ݼ��д��ڵ�ÿ���༶��ȷ����Ŀ S ��ƽ���ɼ������� 3.5 ��ÿ�Ŀû�гɼ���ѧ��������
    //�ڵ�����������ʾÿ���༶����Ϣ��ָʾ�ҵ���ѧ�����������ֿ���Ϊ 0���Ͱ༶��š�
    //��ѧ����������������ݽ������򣬶���ƥ������� - ���༶��ŵĽ�������
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj65");
            string S = GetString();
            string[] subjects = { "Algebra", "Geometry", "Physics" };
            int aa = 0;
            foreach (string subject in subjects)
            {
                if (subjects[aa] == S) ;
                else aa++;
            }
            var culture = new System.Globalization.CultureInfo("en-US");
            var all = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    return new
                    {
                        name = s[0] + " " + s[1],
                        subj = s[2],
                        mark = int.Parse(s[3]),
                        glynna = int.Parse(s[4])
                };
                }).ToArray();
            var stu = all.Select(e => new { name = e.name, glynna = e.glynna })
                         .GroupBy(e => e.name).Select(e => e.FirstOrDefault())
                         .GroupBy(e => e.glynna, (key, ee) => new { key, count = ee.Count() })
                         .OrderByDescending(e => e.key).ToArray().Show();
            var r = all.Where(e => e.subj == subjects[aa])
              .GroupBy(e => e.name, (k, ee) => new{ name = k, avrs = ee.Average(c => c.mark) })
              .Where(e => e.avrs > 3.5);
            var loj = (from e in all
                       join b in r
                       on e.name equals b.name
                       select new
                       {
                           e.name,
                           e.glynna,
                       }).Select(e => new { e.name, e.glynna })
                       .GroupBy(e => e.name).Select(e => e.FirstOrDefault())
                       .GroupBy(e => e.glynna, (key, ee) => new { key, count = ee.Count() })
                       .OrderByDescending(e => e.key).ToArray().Show();

            var fin = (from b in stu
                       join e in loj
                       on b.key equals e.key into G
                       from z in G.DefaultIfEmpty()
                       select new
                       {
                           glynna = b.key,
                           count = (z == null) ? b.count : b.count - z.count
                       })
                       .Select(e => new { e.count, e.glynna }).OrderBy(e => e.count).ThenByDescending(e => e.glynna)
                       .Select(e => e.count + " " + e.glynna).Show();

            File.WriteAllLines(GetString(), fin.ToArray(), Encoding.Default);
        }
    }
}
