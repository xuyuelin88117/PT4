// File: "LinqXml72"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        //����һ�� XML �ĵ������а����йؼ���վ���ͼ۸����Ϣ����һ������Ԫ�أ����ݺ���ͬLinqXml68�����ݰ���˾���Ʒ��飻
        //��˾����ָ��Ϊ��һ��Ԫ�����ƣ���
        //<����>
        //  <price street = "��ڭ���"Ʒ��="92">2200</price>
        //  ...
        //</�쵼>
                    //ͨ�����ֵ����ƺ���ÿ���ֵ��ڰ�����Ʒ�ƶ����ݽ��з�����ת���ĵ������ĵ�һ��Ԫ�����£�
        //<��ڭ���>
        //  <b92>
        //    <min-price company = "Premier-oil" > 2050 </ min - price >
        //    ...
        //  </b92>
        //  ...
        //</��ڭ���>
            
        //��һ��Ԫ��������ֵ�������ͬ���ڶ���Ԫ�����Ʊ�����ǰ׺b������ע������Ʒ�ơ�
        //������Ԫ�ص�ֵ���ڸ����ֵ��ϸ���Ʒ�Ƶ�������ͼ۸��乫˾���԰����ṩ��ͼ۸�ļ���վ�Ĺ�˾���ơ�
        //��һ��Ԫ�ذ��ֵ�������ĸ˳�����У�����Ԫ�ذ����͵ȼ��������С�
        //����м�������Ԫ����һ����ͬ�ĸ�������ͬһ�������м�������վ����Ʒ�Ƶ����ͼ۸���ͣ�����ô��ЩԪ��Ӧ�ð��չ�˾���Ƶ���ĸ˳�����С�
        public static void Solve()
        {
            Task("LinqXml72");
            string fileName = GetString();
            var d = XDocument.Load(fileName);
            var a = d.Root.Name.Namespace;
            var data = d.Root.Descendants(a + "price").Select(el => new
            {
                company = el.Parent.Name.LocalName,
                street = el.Attribute("street").Value,
                brand = int.Parse(el.Attribute("brand").Value),
                price = el.Value
            }).OrderBy(e =>e.street).ThenBy(e => e.brand).ThenBy(e => e.company)
            .GroupBy(e => new { e.street, e.brand })
            .SelectMany(x => x.OrderBy(y => y.price).GroupBy(y => y.price).Take(1)).SelectMany(ee => ee);
            d.Root.ReplaceNodes(data.GroupBy(n => n.street,
                                               (n, el) => new XElement(a + n.ToString(),
                                                     el.GroupBy(m => m.brand,
                                               (m, e2) => new XElement(a + "b" + m.ToString(),
                                                     e2.GroupBy(q => q.company,
                                               (q, e3) => new XElement(a + "min-price", new XAttribute("company", q),
                                                    e3.Select(e => e.price.ToString())
                                                )))))));

            d.Save(fileName);
        }
    }
}
