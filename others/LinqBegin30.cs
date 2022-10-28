using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin30 : ITask
    {
        public void RunTask()
        {
            var k = 6;
            var enumerable = Enumerable.Range(1, 15);
            var evenEnumerable = enumerable.Where(num => num.IsEven());
            var skipBeforeKEnumerable = enumerable.Skip(k);
            
        }
    }
}