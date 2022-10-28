using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin17 : ITask
    {
        public void RunTask()
        {
            var arr = new []{1, 3, 4, -5, 83, 23, 12, -3, 2, 12, 14};
            
            var evenUniqueNumbers = arr.Where(num => num % 2 == 0).Distinct();
            Console.WriteLine(string.Join(' ', evenUniqueNumbers));
        }
    }
}