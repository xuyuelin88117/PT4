using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin23 : ITask
    {
        public void RunTask()
        {
            var K = 5;
            var enumerable = Enumerable.Range(1, 20);
            var orderedSliceNumbers = enumerable.Skip(K)
                .Where(num => num % 2 == 1 && num / 10 > 0)
                .OrderByDescending(_ => _);

            Console.WriteLine($"Numbers:\n{string.Join(' ', orderedSliceNumbers)}");
        }
    }
}