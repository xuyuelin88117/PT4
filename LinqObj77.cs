// File: "LinqObj77"
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
            Task("LinqObj77");
            var dataB = File.ReadLines(GetString(), Encoding.Default).Select(x =>
            {
                var arr = x.Split(' ');
                return new
                {
                    Catrgory = arr[0],
                    SKU = arr[1],
                    CountryOfOrigin = arr[2]
                };
            });

            var dataD = File.ReadLines(GetString(), Encoding.Default).Select(x =>
            {
                var arr = x.Split(' ');
                return new
                {
                    SKU = arr[0],
                    Price = arr[1],
                    StoreName = arr[2],
                };
            });

            var r = dataB.Join(dataD,
                       b => b.SKU,
                       d => d.SKU, (b, d) => new
                       {
                           SKU = b.SKU,
                           Catrgory = b.Catrgory,
                           CountryOfOrigin = b.CountryOfOrigin,
                           Price = d.Price,
                           StoreName = d.StoreName
                       }).GroupBy(x => x.Catrgory)
                         .Select(x => new
                         {
                             Catrgory = x.Key,
                             StoreCount = x.GroupBy(m => m.StoreName).Count(),
                             CountryOfOriginCount = x.GroupBy(m => m.StoreName).Count()
                         })
                         .OrderByDescending(x => x.StoreCount)
                         .ThenBy(x => x.Catrgory)
                         .ToList();

            File.WriteAllLines(GetString(), r.Select(x => $"{x.StoreCount} {x.Catrgory} {x.CountryOfOriginCount}"));

        }
    }
}
