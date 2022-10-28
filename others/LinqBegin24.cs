using System;
using System.Linq;

namespace DevIncubatorCore.Linq.LinqBegin
{
    public class LinqBegin24 : ITask
    {
        public void RunTask()
        {
            var k = 6;
                          var arr = new[]
                          {
                              "enumerable.Range",
                              "string",
                              "Hello",
                              "Help",
                              "System.Collections.Generic",
                              "System.Linq",
                              "List",
                              "Yes",
                              "No"
                          };
            var result = arr.Take(k)
                .Where(str => str.Length % 2 == 1 &&
                    char.IsUpper(str.First()))
                .OrderByDescending(str => str);
            
            Console.WriteLine(string.Join('\n', result));
        }
    }
}