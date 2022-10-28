// File: "LinqXml77"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace PT4Tasks
{
    //�ṩ��һ�� XML �ĵ������а����йع�����ҵ�˵�����Ϣ����һ�������Ԫ�أ����ݺ���ͬLinqXml76�У���
    //<debt house = "12" flat="129" name="Sergeev T.M.">1833.32</debt>
    //ͨ�����ĵ�һ��Ԫ����ת���ĵ���������ʾ��
    //<address12-129 name="Sergeev T.M."ծ��="1833.32" />
    //Ԫ�����Ʊ����е�ַǰ׺��������ƺţ��������ۺű�ʾ��Ԣ�š�
    //Ԫ������ϱ�ǩ��ʾ�����ұ��밴���ƺŵ��������򣬶�����ͬ�����ƺ� - ����Ԣ�ŵ���������
    
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqXml77");
            string fileName = GetString();
            var d = XDocument.Load(fileName);
            var a = d.Root.Name.Namespace;
            d.Root.ReplaceNodes(d.Root.Elements()
            .OrderBy(e => int.Parse(e.Attribute("house").Value)).ThenBy(e => int.Parse(e.Attribute("flat").Value)).Select(e => 
            new XElement(a + ("address"+ e.Attribute("house").Value.ToString() + "-" + e.Attribute("flat").Value.ToString()),
                                                                               new XAttribute("name", e.Attribute("name").Value),
                                                                               new XAttribute("debt",e.Value))));
            d.Save(fileName);
        }
    }
}
