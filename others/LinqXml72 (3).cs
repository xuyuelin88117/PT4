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
        //给定一个 XML 文档，其中包含有关加油站汽油价格的信息。第一级样本元素（数据含义同LinqXml68，数据按公司名称分组；
        //公司名称指定为第一级元素名称）：
        //<领袖>
        //  <price street = "契诃夫街"品牌="92">2200</price>
        //  ...
        //</领导>
                    //通过按街道名称和在每条街道内按汽油品牌对数据进行分组来转换文档。更改第一级元素如下：
        //<契诃夫街>
        //  <b92>
        //    <min-price company = "Premier-oil" > 2050 </ min - price >
        //    ...
        //  </b92>
        //  ...
        //</契诃夫街>
            
        //第一层元素名称与街道名称相同，第二层元素名称必须有前缀b，后面注明汽油品牌。
        //第三级元素的值等于给定街道上给定品牌的最低汽油价格，其公司属性包含提供最低价格的加油站的公司名称。
        //第一级元素按街道名称字母顺序排列，其子元素按汽油等级升序排列。
        //如果有几个三级元素有一个共同的父级（即同一条街上有几个加油站，该品牌的汽油价格最低），那么这些元素应该按照公司名称的字母顺序排列。
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
