using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin15 : ITask
    {
        /// <summary>
        ///     LinqBegin15
        /// </summary>
        public void RunTask()
        {
            var N = 5;
            var enumerable = Enumerable.Range(1, N);
            var factorial = enumerable.Aggregate((x, y) => x * y);

            Console.WriteLine(factorial);
        }
    }
}