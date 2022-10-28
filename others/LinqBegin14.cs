using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin14 : ITask
    {
        /// <summary>
        ///     Average Arithmetic Squares Range
        /// </summary>
        public void RunTask()
        {
            var N = 5;
            
            var enumerable = Enumerable.Range(1, N);
            var average = enumerable.Select(x => x * x).Average();

            Console.WriteLine(average);
        }
    }
}