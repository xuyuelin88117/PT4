// File: "LinqXml85"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    //给定一个 XML 文档，其中包含有关不同学科学生成绩的信息。第一层的样本元素（数据含义同LinqXml83中）：
    //<info class="9" name="Stepanova DB"主题="物理" 标记="4" />
    //通过按班级编号、按学生在每个班级内以及按学科对每个学生分组数据来转换文档。更改第一级元素如下：

//<classnumber="9">
//  <瞳孔名称="Stepanova D.B.">
//    <subject name = "物理" >
//      < 标记 > 4 </ 标记 >
//      ...
//    </主题>
//    ...
//  </瞳孔>
//  ...
//</class>

    //第一级的元素应该按班级编号的升??序排列，他们的孩子 - 按学生的姓氏和首字母的字母顺序排列。
    //具有共同父项的三级元素应按学科名称字母顺序排序，具有共同父项的四级项目应按年级降序排序。

    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqXml85");
            string fileName = GetString();
            var d = XDocument.Load(fileName);
            var a = d.Root.Name.Namespace;
            var data = d.Root.Elements().Select(el => new {
                name = el.Attribute("name").Value,
                klass = int.Parse(el.Attribute("class").Value),
                mark = el.Attribute("mark").Value,
                subject = el.Attribute("subject").Value
            }).OrderBy(el => el.klass).ThenBy(e1 => e1.name).ThenBy(el => el.subject).ThenByDescending(el => el.mark);


            d.Root.ReplaceNodes(data.GroupBy(n => n.klass,
                                               (n, el) => new XElement(a + "class", new XAttribute("number", n),
                                                    el.GroupBy(m => m.name,
                                               (m, e2) => new XElement(a + "pupil", new XAttribute("name", m),
                                                    e2.GroupBy(q => q.subject,
                                               (q, e3) => new XElement(a + "subject", new XAttribute("name", q),
                                                    e3.Select(e => new XElement(a + "mark", e.mark)))))))));
            d.Save(fileName);
        }
    }
}
