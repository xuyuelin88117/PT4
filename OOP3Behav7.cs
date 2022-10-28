// File: "OOP3Behav7"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Aggregate
        {
            public abstract Iterator CreateIterator();
        }

        // Implement the ConcreteAggregateA, ConcreteAggregateB
        //   and ConcreteAggregateC descendant classes
        public class ConcreteAggregateA : Aggregate
        {
            public ConcreteAggregateA(int data) => this.data = data;
            public override Iterator CreateIterator() => new ConcreteIteratorA(this);
            public int data { get; set; }
            public int divied { get; set; } = 1;
        }

        public class ConcreteAggregateB : Aggregate
        {
            public ConcreteAggregateB(string data) => this.data = data;
            public override Iterator CreateIterator() => new ConcreteIteratorB(this);
            public string data { get; set; }
            public int index { get; set; } = 0;
        }

        public class ConcreteAggregateC : Aggregate
        {
            public ConcreteAggregateC(int[] data) => this.data = data;
            public override Iterator CreateIterator() => new ConcreteIteratorC(this);
            public int[] data { get; set; }
            public int index { get; set; } = 0;
            public int divied { get; set; } = 1;
        }
        public abstract class Iterator
        {
            public abstract void First();
            public abstract void Next();
            public abstract bool IsDone();
            public abstract int CurrentItem();
        }

        // Implement the ConcreteIteratorA, ConcreteIteratorB
        //   and ConcreteIteratorC descendant classes
        public class ConcreteIteratorA : Iterator
        {
            public ConcreteIteratorA(ConcreteAggregateA aggr) => this.a = aggr;
            public override void First() => a.divied = 1;
            public override void Next() => a.divied *= 10;
            public override bool IsDone()
                => a.data != 0 && a.data / a.divied == 0 || a.data == 0 && a.divied == 10;
            public override int CurrentItem() => Math.Abs(a.data / a.divied % 10);

            private ConcreteAggregateA a;
        }

        public class ConcreteIteratorB : Iterator
        {
            public ConcreteIteratorB(ConcreteAggregateB a) => this.a = a;

            public override void First()
            {
                a.index = a.data.Length - 1;
                while (a.index >= 0 && !char.IsDigit(a.data[a.index]))
                {
                    a.index--;
                }
            }

            public override void Next()
            {
                a.index--;
                while (a.index >= 0 && !char.IsDigit(a.data[a.index]))
                {
                    a.index--;
                }
            }

            public override bool IsDone() => a.index < 0;
            public override int CurrentItem() => a.data[a.index] - '0';

            private ConcreteAggregateB a;
        }

        public class ConcreteIteratorC : Iterator
        {
            public ConcreteIteratorC(ConcreteAggregateC a) => this.a = a;

            public override void First()
            {
                a.index = a.data.Length - 1;
                a.divied = 1;
            }

            public override void Next()
            {
                a.divied *= 10;
                int data = a.data[a.index];
                int divied = a.divied;
                if (data != 0 && data / divied == 0 || data == 0 && divied == 10)
                {
                    a.index--;
                    a.divied = 1;
                }
            }

            public override bool IsDone() => a.index < 0;
            public override int CurrentItem() => Math.Abs(a.data[a.index] / a.divied % 10);

            private ConcreteAggregateC a;
        }
        public static void Solve()
        {
            Task("OOP3Behav7");
            int num = GetInt();
            var a = new Aggregate[num];
            for (int i = 0; i < num; i++)
            {
                char type = GetChar();
                if (type == 'A')
                {
                    int data = GetInt();
                    a[i] = new ConcreteAggregateA(data);
                }
                else if (type == 'B')
                {
                    string data = GetString();
                    a[i] = new ConcreteAggregateB(data);
                }
                else
                {
                    int n = GetInt();
                    int[] data = new int[n];
                    for (int j = 0; j < n; j++)
                    {
                        int number = GetInt();
                        data[j] = number;
                    }
                    a[i] = new ConcreteAggregateC(data);
                }
            }

            foreach (var i in a.Reverse())
            {
                var digit = new List<int>();
                var it = i.CreateIterator();
                for (it.First(); !it.IsDone(); it.Next())
                {
                    digit.Add(it.CurrentItem());
                }
                Put(digit.Sum());
                foreach (var d in digit)
                {
                    Put(d);
                }

            }
        }
    }
}
