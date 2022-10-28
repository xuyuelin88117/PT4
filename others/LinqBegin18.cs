using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin18 : ITask
    {
        public void RunTask()
        {
            var arr = new []{1, 3, 4, -4, 83, 23, 12, -3, -8};

            var negativeEvenNumbers = arr.Where(num => num < 0 && num % 2 == 0).ToArray();

            Console.WriteLine(string.Join(' ', negativeEvenNumbers.Reverse()));
        }
    }
}