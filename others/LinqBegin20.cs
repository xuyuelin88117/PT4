using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin20 : ITask
    {
        public void RunTask()
        {
            var arr = new []{1, 3, 3, 23, 83, 4, -5, 83, 23, 12, -3};
            var positiveTwoDigitOrdered = arr.Where(num => num > 0 && num / 10 != 0).OrderBy(_ => _);
            Console.WriteLine(string.Join(' ', positiveTwoDigitOrdered));
        }
    }
}