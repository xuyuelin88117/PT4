// File: "LinqObj22"
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
            Task("LinqObj22");
            var r = File.ReadLines(GetString(), Encoding.Default).Select(x =>
                          {
                              var arr = x.Split(' ');
                              return new
                              {
                                  name = arr[0],
                                  numberOfSchool = Convert.ToInt32(arr[1]),
                                  yearOfAamission = Convert.ToInt32(arr[2])
                              };
                          }).OrderBy(x => x.numberOfSchool)
                          .GroupBy(x => x.numberOfSchool)
                          .ToList();

            File.WriteAllLines(GetString(), r.Select(x => $"{x.Key} {string.Join(" ", x.OrderBy(m => m.yearOfAamission).Select(m => m.yearOfAamission))}"));
        }
    }
}
