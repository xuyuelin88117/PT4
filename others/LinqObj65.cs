// File: "LinqObj65"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //给定一个字符串 S——三个科目之一的名称：代数、几何或物理。源序列包含有关这三个科目中学生成绩的信息。
    //该序列的每个元素都包含有关一项评估的数据，并包括以下字段：
    //<姓氏> <姓名缩写> <项目名称> <等级> <班级>
    //学生之间没有完整的同名（姓氏和首字母相同）。
    //班级指定为整数，分数是 2-5 范围内的整数。项目名称用大写字母表示。
    //对于初始数据集中存在的每个班级，确定科目 S 的平均成绩不超过 3.5 或该科目没有成绩的学生人数。
    //在单独的行上显示每个班级的信息，指示找到的学生人数（数字可以为 0）和班级编号。
    //按学生人数的升序对数据进行排序，对于匹配的数字 - 按班级编号的降序排序。
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
