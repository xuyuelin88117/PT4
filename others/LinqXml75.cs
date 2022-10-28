// File: "LinqXml75"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
// LinqXml75°。给出了一个包含加油站价格信息的XML文档。
// 一个第一级元素的样本（数据的含义与LinqXml68相同，公司名称和街道名称用下划线隔开，作为第一级元素名称）。
// < Leader_Chekhov - St.>
//   < brand > 92 </ brand >
//   < price > 2200 </ price >
// </ Leader_Chekhov - St.>
// 通过按街道名称对数据进行分组，并对第一层元素进行如下修改，来转换该文件。
// 第一层元素的名称与街道名称相吻合，第二层元素的名称有一个前缀品牌，在这个前缀品牌之后是指定的汽油品牌。
// 站数属性等于位于给定街道上提供给定品牌汽油的加油站数量；
// 第二级元素的值是位于给定街道上所有加油站的1升给定品牌汽油的平均价格。平均价格由以下公式得出。
// "所有站的总价" / "站的数量"，其中 "/"表示整数除法。
// 如果在给定的街道上没有提供给定品牌汽油的加油站，那么第二层的相应元素的值和其属性station - count的值应该等于0。
// 第一层元素应按街道名称的字母顺序排序，其子元素按汽油品牌的降序排序。
namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqXml75");
            string name = GetString();
            var d = XDocument.Load(name);
            XNamespace ns = d.Root.Name.Namespace;
            var res = d.Root.Elements()
            .Select(e => new
            {
                company = e.Name.LocalName.Split('_')[0],
                street = e.Name.LocalName.Split('_')[1] +"_"+ e.Name.LocalName.Split('_')[2],
                brand = int.Parse(e.Element("brand").Value),
                price = int.Parse(e.Element("price").Value),
            })
            .OrderBy(e=>e.street)
            .ThenByDescending(e=>e.brand)
            .Show();
            IEnumerable<int> oilbran = new int[]{92,95,98};
            d.Root.ReplaceNodes(res.GroupBy(e=>e.street,(street,list)=>
                new XElement(ns+street,
                oilbran.OrderByDescending(e=>e)
                    .GroupJoin(list,e=>e,e=>e.brand,(brand,sublist)=>
                        new XElement(ns+ "brand" + brand.ToString(),
                            new XAttribute("station-count",sublist.Count()),
                            sublist.Count()==0 ? 0 : (sublist.Sum(e=>e.price))/(sublist.Count())
                        )
                    )
                )
            ));
            d.Save(name);

        }
    }
}
