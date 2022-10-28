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
    //����һ�� XML �ĵ������а����йز�ͬѧ��ѧ���ɼ�����Ϣ��
    //һ��Ԫ��ʾ�������ݺ�����LinqXml83����ͬ��һ��Ԫ������Ϊѧ����������������ĸ�����Ϻ���������ĸ֮��Ŀո����»��ߴ��棩 ��
    //<�˵��޷�_S.N. class="11" subject="Physics">4</Petrov_S.N.>
    //ͨ������������ݽ��з��飬��������ÿ��������з�����ת���ĵ������ĵ�һ��Ԫ�����£�

//<����>
//  <class7 student-count="0" mark-count="0" />
//  ...
//  <class11 student-count="3" mark-count="5" />
//</����>

    //��һ��Ԫ������������������ͬ���ڶ���Ԫ�����Ʊ�������ǰ׺�����š�
    //student-count ���Ե�ֵ���ڸð༶�иÿ�Ŀ������??һ��������ѧ��������mark-count ���Ե�ֵ���ڸð༶�ÿ�Ŀ�иÿ�Ŀ�ķ�����
    //����ÿ����Ŀ��Ӧ��ʾÿ���༶����Ϣ���� 7 �� 11����
    //����ڸ���ѧ�Ƶ�ĳ���༶��û��ѧ�������飬��ôѧ�����ͷ�������Ӧ�õ���0��
    //��һ��Ԫ��Ӧ�ð�ѧ�����Ƶ���ĸ˳����������Ԫ��Ӧ������ͨ��������š�

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
