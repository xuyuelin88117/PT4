// File: "LinqObj84"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public static void Solve()
        {
            Task("LinqObj84");
            var dataC = File.ReadLines(GetString(), Encoding.Default).Select(x =>
                        {
                            var arr = x.Split(' ');
                            return new
                            {
                                Discount = Convert.ToInt32(arr[0]),
                                StoreName = arr[1],
                                CustomerCode = arr[2]
                            };
                        });

            var dataD = File.ReadLines(GetString(), Encoding.Default).Select(x =>
            {
                var arr = x.Split(' ');
                return new
                {
                    SKU = arr[0],
                    StoreName = arr[1],
                    Price = Convert.ToInt32(arr[2]),
                };
            });

            var dataE = File.ReadLines(GetString(), Encoding.Default).Select(x =>
            {
                var arr = x.Split(' ');
                return new
                {
                    SKU = arr[0],
                    StoreName = arr[1],
                    CustomerCode = arr[2],
                };
            });

           var r = dataD.GroupJoin(dataE,
                       d => new { StoreName = d.StoreName, SKU = d.SKU },
                       e => new { StoreName = e.StoreName, SKU = e.SKU },
                       (d, e) => e.DefaultIfEmpty(new
                       {
                           SKU = d.SKU,
                           StoreName = d.StoreName,
                           CustomerCode = ""
                       }).Select(x => new
                       {
                           StoreName = d.StoreName,
                           SKU = d.SKU,
                           Price = d.Price,
                           CustomerCode = x.CustomerCode,
                       })).SelectMany(x => x)
                       .Where(x => !string.IsNullOrEmpty(x.CustomerCode))
                       .GroupJoin(dataC,
                       t => new { CustomerCode = t.CustomerCode, StoreName = t.StoreName },
                       c => new { CustomerCode = c.CustomerCode, StoreName = c.StoreName },
                       (t, c) => c.DefaultIfEmpty(new
                       {
                           Discount = 0,
                           StoreName = t.StoreName,
                           CustomerCode = t.CustomerCode
                       }).Select(x => new
                       {
                           StoreName = t.StoreName,
                           SKU = t.SKU,
                           Price = t.Price,
                           CustomerCode = t.CustomerCode,
                           Discount = x.Discount
                       })).SelectMany(x => x)
                       .Where(x => x.Discount > 0)
                       .OrderBy(x=>x.StoreName)
                       .ThenBy(x=>x.SKU)
                       .GroupBy(x => new { StoreName = x.StoreName, SKU = x.SKU })
                       .Select(x => new { StoreName = x.Key.StoreName, SKU = x.Key.SKU, Count = x.Count(), Cost =x.Sum(m =>Math.Ceiling(m.Price * (100-m.Discount)/100m))})
                       .ToList();

            string s = GetString();
            if (r.Count > 0)
            {
                File.WriteAllLines(s, r.Select(x => $"{x.StoreName} {x.SKU} {x.Count} {x.Cost}"));
                
                r.ForEach(x => Console.WriteLine($"{x.StoreName} {x.SKU} {x.Count} {x.Cost}"));
            }
            else
            {
                File.WriteAllText(s, "Required data not found");
            }
        }
    }
}
