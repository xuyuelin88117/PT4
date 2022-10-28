// File: "OOP3Behav6"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class AbstractComparable
        {
            public abstract int CompareTo(AbstractComparable other);

            // Implement the IndexMax, LastIndexMax, IndexMin
            //   and LastIndexMin static methods
            public int IndexMax(List <AbstractComparable> comp)
            {
                int index = 0;
                var max = comp[0];
                for (int i = 0; i < comp.Count; i++)
                {
                    if (comp[i].CompareTo(max) > 0)
                    {
                        index = i;
                        max = comp[i];
                    }
                }
                return index;
            }
            public int LastIndexMax(List <AbstractComparable> comp)
            {
                int index = 0;
                var max = comp[0];
                for (int i = 0; i < comp.Count; i++)
                {
                    if (comp[i].CompareTo(max) >= 0)
                    {
                        index = i;
                        max = comp[i];
                    }
                }
                return index;
            }

            public int IndexMin(List<AbstractComparable> comp)
            {
                int index = 0;
                var min = comp[0];
                for (int i = 0; i < comp.Count; i++)
                {
                    if (comp[i].CompareTo(min) < 0)
                    {
                        index = i;
                        min = comp[i];
                    }
                }
                return index;
            }
            public int LastIndexMin(List<AbstractComparable> comp)
            {
                int index = 0;
                var min = comp[0];
                for (int i = 0; i < comp.Count; i++)
                {
                    if (comp[i].CompareTo(min) <= 0)
                    {
                        index = i;
                        min = comp[i];
                    }
                }
                return index;
            }
        }
        

        // Implement the NumberComparable, LengthComparable
        //   and TextComparable descendant classes

        public class NumberComparable : AbstractComparable
        {
            private int index;
            public NumberComparable(string str)
            {
                if(str == "")
                    index = 0;
                else
                {
                    int a;
                    if (int.TryParse(str, out a))
                        index = int.Parse(str);
                    else
                        index = 0;
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '-')
                            continue;
                        else if(str[i] < '0'|| str[i] > '9')
                        {
                            index = 0;
                            break;
                        }
                    }
                }
            }
            public override int CompareTo(AbstractComparable other)
            {
                NumberComparable n = other as NumberComparable;
                if (this.index == n.index)
                    return 0;
                else if (this.index > n.index)
                    return 1;
                else return -1;
            }
        }
        
        public class LengthComparable : AbstractComparable
        {
            private int index;
            public LengthComparable(string str)
            {
                index = str.Length;
            }
            public override int CompareTo(AbstractComparable other)
            {
                LengthComparable n = other as LengthComparable;
                if (this.index == n.index)
                    return 0;
                else if (this.index > n.index)
                    return 1;
                else return -1;
            }
        }
        public class TextComparable : AbstractComparable
        {
            private string index;
            public TextComparable(string index)
            {
                this.index = index; 
            }
            public override int CompareTo(AbstractComparable other)
            {
                TextComparable n = other as TextComparable;
                if (String.CompareOrdinal(this.index, n.index) == 0)
                    return 0;
                else if (String.CompareOrdinal(this.index, n.index) > 0)
                    return 1;
                else return -1;
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav6");
            int N = GetInt();
            int K = GetInt();
            for(int i =0; i < K; i++)
            {
                string str = GetString();
                List <AbstractComparable> list = new List<AbstractComparable>();
                for (int j = 0; j < N; j++)
                {
                    string s = GetString();
                    AbstractComparable a = null;
                    if (str == "N")
                        a = new NumberComparable(s);
                    else if (str == "L")
                        a = new LengthComparable(s);
                    else if (str == "T")
                        a = new TextComparable(s);
                    list.Add(a);
                }
                Put(list[0].IndexMax(list));
                Put(list[0].LastIndexMax(list));
                Put(list[0].IndexMin(list));
                Put(list[0].LastIndexMin(list));
            }

        }
    }
}
