using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin25 : ITask
    {
        public void RunTask()
        {
            var k1 = 2;
            var k2 = 5;
            var enumerable = Enumerable.Range(-1, 8);

            var sumEnumerable = enumerable.Skip(k1)
                .Take(k2 - k1)
                .Where(val => val > 0)
                .Sum();

            Console.WriteLine(sumEnumerable);
        }
    }
}