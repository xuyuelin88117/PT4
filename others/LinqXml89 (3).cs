// File: "LinqXml89"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    //给定一个 XML 文档，其中包含有关不同学科学生成绩的信息。
    //一级元素示例（数据含义与LinqXml83中相同，一级元素名称为学生姓名和姓名首字母；姓氏和姓名首字母之间的空格用下划线代替） ：
    //<彼得罗夫_S.N. class="11" subject="Physics">4</Petrov_S.N.>
    //通过按主题对数据进行分组，并按类别对每个主题进行分组来转换文档。更改第一级元素如下：

//<物理>
//  <class7 student-count="0" mark-count="0" />
//  ...
//  <class11 student-count="3" mark-count="5" />
//</物理>

    //第一级元素名称与主题名称相同，第二级元素名称必须有类前缀后跟类号。
    //student-count 属性的值等于该班级中该科目至少有??一个分数的学生人数，mark-count 属性的值等于该班级该科目中该科目的分数。
    //对于每个科目，应显示每个班级的信息（从 7 到 11）；
    //如果在给定学科的某个班级中没有学生被调查，那么学生数和分数属性应该等于0。
    //第一级元素应该按学科名称的字母顺序排序，其子元素应该排序通过升序类号。

    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqXml89");
            string fileName = GetString();
            var d = XDocument.Load(fileName);
            var ns = d.Root.Name.Namespace;
            var kk = new int[] { 7, 8, 9, 10, 11 };
            var data = d.Root.Elements().Select(el => new
            {
                name = el.Name.LocalName,
                klass = int.Parse(el.Attribute("class").Value),
                mark = el.Value,
                subject = el.Attribute("subject").Value
            }).OrderBy(e => e.subject).ThenBy(e => e.klass).Show();

            d.Root.ReplaceNodes(data.GroupBy(e => e.subject, (subject, list) =>
                  new XElement(ns + subject,
                  kk.OrderBy(e => e)
                      .GroupJoin(list, e => e, e => e.klass, (klass, list2) =>
                                 new XElement(ns + "class" + klass.ToString(),
                                     new XAttribute("pupil-count", list2.GroupBy(ad => ad.name).Count()), 
                                         new XAttribute("mark-count", list2.Select(ad => ad.mark).Count()))))));

            d.Save(fileName);


        }

    }
}
