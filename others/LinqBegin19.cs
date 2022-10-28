using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin19 : ITask
    {
        public void RunTask()
        {
            const int d = 3;
            var arr = new []{1, 3, 3, 23, 83, 4, -5, 83, 23, 12, -3};
            var positiveEndingDUniqueNumbers  = arr.Where(num => num > 0)
                .Where(num => num % 10 == d)
                .Distinct();

            Console.WriteLine(string.Join(' ', positiveEndingDUniqueNumbers));
        }
    }
}