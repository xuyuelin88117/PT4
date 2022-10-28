using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin16 : ITask
    {
        public void RunTask()
        {
            var arr = new []{1, 3, 4, -5, 83, 23, 12, -3};
            var positiveNumbers = arr.Where(num => num > 0);
            Console.WriteLine(string.Join(' ', positiveNumbers));
        }
    }
}