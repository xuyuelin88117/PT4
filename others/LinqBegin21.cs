using System;
using System.Collections.Generic;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin21 : ITask
    {
        public void RunTask()
        {
            var arr = new[]
            {
                "ABCDSKJFDKSL KLJDSFLJK KJDFS LKFS DJLK",
                "ABCLJDSFLJKFS DJLK",
                "ABCLJDSFLJKFS DJLK",
                "ABCLJDSFLJK KJDFS LKFS DJLK",
                "ABCDSKJFDKSL KLJDSFLJKKFS DJLK",
                "ABCDSKJFDKSL KLJDSFLJKJLK",
                "ABCDSKJFDKS KJDFS LKFS DJLK",
                "ABCDSKJFDKS KJDFS LKFS DJLK",
                "ABCDSKJFDKS KJDFS LKFS DJLK",
            };
            var orderedEnumerable = arr.OrderBy(str => str.Length);
            Console.WriteLine($"Ordered by count - {string.Join('\n', orderedEnumerable)}\n");

            var repeatRows = arr.GroupBy(_=>_)
                .Where(el => el.Count() > 1)
                .Select(el => el.Key)
                .ToList();
            
            var orderedByDescendingNonUniqueStrings = arr.Select(el => 
                    repeatRows.Where(row => row.Equals(el)))
                .SelectMany(repeatStrings => repeatStrings)
                .OrderByDescending(row=>row.Length)
                .ToList();

            Console.WriteLine($"Ordered by descending non unique strings : \n{string.Join('\n', orderedByDescendingNonUniqueStrings)}");
        }
    }
}