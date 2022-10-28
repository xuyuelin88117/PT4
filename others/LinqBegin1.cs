using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    internal class LinqBegin1 : ITask
    {
        // First Positive Last Negative
        public void RunTask()
        {
            var arr = new[] {1, 3, 5, -2, -4, 4, -1, -5, 2, 10};

            Console.WriteLine(arr.First(x => x > 0));
            Console.WriteLine(arr.Last(x => x < 0));
        }
    }
}