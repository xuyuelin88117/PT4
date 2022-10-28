// File: "LinqObj27"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    //初始序列包含有关居住在 144 间公寓的 9 层建筑中的水电费债务人的信息。每个序列元素包括以下字段：
    //<姓氏> <公寓号码> <债务>
    //债务以小数表示（全部为卢布，小数部分为戈比）。每个楼层的每个入口都有 4 套公寓。
    //对于房子的 9 层，每层都显示居住在该楼层的债务人的信息：债务人数量、楼层数、该楼层居民的总债务（以两个分数符号显示）。
    //在单独的行上显示有关每个楼层的信息，并按债务人数量的升序排序，对于匹配的数字 - 按楼层的升序排序。
    //如果任何楼层都没有债务人，则不要显示有关该楼层的数据。
    //一共4个公寓，每个公寓9层，每个公寓每层4个房间，从1号公寓开始编号，二号公寓一楼第一个就是37号。
    //先确定公寓是哪个，再确定场所
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj27");
            var r = File.ReadLines(GetString(), Encoding.Default)
                .Select(e =>
                {
                    string[] s = e.Split(' ');
                    int c = int.Parse(s[1]) % 36;
                    int d = 0;
                    if (c == 0)
                        d = 9;
                    else if (c % 4 == 0)
                        d = c / 4;
                    else if (c % 4 != 0)
                        d = c / 4 + 1;
                    return new
                    {
                        name = s[0],
                        code = d,
                        money = float.Parse(s[2]),
                    };
                }).GroupBy(e => e.code,(k, ee) => new { k, sum = ee.Sum(c => c.money),count = ee.Count()})
                  .OrderBy(e => e.count).ThenBy(e => e.k).Select(e => e.count.ToString() + " " + e.k + " " + e.sum.ToString("#0.00"));
            File.WriteAllLines(GetString(), r.ToArray(), Encoding.Default);
        }
    }
}
