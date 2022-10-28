using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin12 : ITask
    {
        public void RunTask()
        {
            var enumerable = new[]{12, 15, 19, 3};
            
            Console.WriteLine(enumerable.Select(x => x % 10).Aggregate(1.0, (a, b) => a * b));
        }
    }
}