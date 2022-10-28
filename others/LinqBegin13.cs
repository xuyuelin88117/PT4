using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin13 : ITask
    {
        /// <summary>
        ///     Sum LinqBegin15 Divider
        /// </summary>
        public void RunTask()
        {
            var N = 6;
            var arr = Enumerable.Range(1, N);

            var sum = arr.Select(x => 1.0 / x).Sum();
            Console.WriteLine(Math.Round(sum, 2));
        }
    }
}