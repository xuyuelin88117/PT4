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
    //提供了一个 XML 文档，其中包含有关公用事业账单的信息。第一层的样本元素（数据含义同LinqXml76中）：
    //<debt house = "12" flat="129" name="Sergeev T.M.">1833.32</debt>
    //通过更改第一级元素来转换文档，如下所示：
    //<address12-129 name="Sergeev T.M."债务="1833.32" />
    //元素名称必须有地址前缀，后跟门牌号，并用破折号表示公寓号。
    //元素由组合标签表示，并且必须按门牌号的升序排序，对于相同的门牌号 - 按公寓号的升序排序。
    
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
