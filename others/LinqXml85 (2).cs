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
    //����һ�� XML �ĵ������а����йز�ͬѧ��ѧ���ɼ�����Ϣ����һ�������Ԫ�أ����ݺ���ͬLinqXml83�У���
    //<info class="9" name="Stepanova DB"����="����" ���="4" />
    //ͨ�����༶��š���ѧ����ÿ���༶���Լ���ѧ�ƶ�ÿ��ѧ������������ת���ĵ������ĵ�һ��Ԫ�����£�

//<classnumber="9">
//  <ͫ������="Stepanova D.B.">
//    <subject name = "����" >
//      < ��� > 4 </ ��� >
//      ...
//    </����>
//    ...
//  </ͫ��>
//  ...
//</class>

    //��һ����Ԫ��Ӧ�ð��༶��ŵ���??�����У����ǵĺ��� - ��ѧ�������Ϻ�����ĸ����ĸ˳�����С�
    //���й�ͬ���������Ԫ��Ӧ��ѧ��������ĸ˳�����򣬾��й�ͬ������ļ���ĿӦ���꼶��������

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
