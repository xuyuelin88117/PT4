// File: "OOP1Creat9"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Builder
        {
            public virtual void BuildStart() { }
            public virtual void BuildPartA() { }
            public virtual void BuildPartB() { }
            public virtual void BuildPartC() { }
            public abstract string GetResult();
        }

        // Implement the ConcreteBuilder1
        //   and ConcreteBuilder2 descendant classes
        public class ConcreteBuilder1 : Builder
        {
            public string info;

            public ConcreteBuilder1() { this.info = ""; }
            public override void BuildStart()
            {
                this.info = "";
            }
            public override void BuildPartA()
            {
                this.info = info + "-1-";
            }
            public override void BuildPartB()
            {
                this.info = info + "-2-";
            }
            public override void BuildPartC()
            {
                this.info = info + "-3-";
            }
            public override string GetResult()
            {
                return info;
            }
        }
        public class ConcreteBuilder2 : Builder
        {
            string info;

            public ConcreteBuilder2() { this.info = ""; }
            public override void BuildStart()
            {
                this.info = "";
            }
            public override void BuildPartA()
            {
                this.info = info + "=*=";
            }
            public override void BuildPartB()
            {
                this.info = info + "=**=";
            }
            public override void BuildPartC()
            {
                this.info = info + "=***=";
            }
            public override string GetResult()
            {
                return info;
            }
        }
        public class Director
        {
            Builder b;
            public Director(Builder b)
            {
                this.b = b;
            }
            public string GetResult()
            {
                return b.GetResult();
            }
            public void Construct(string templat)
            {
                // Implement the method
                b.BuildStart();
                int i;
                for (i = 0; i < templat.Length; i++)
                {
                    if (templat[i] == 'A')
                    {
                        b.BuildPartA();
                    }
                    if (templat[i] == 'B')
                    {
                        b.BuildPartB();
                    }
                    if (templat[i] == 'C')
                    {
                        b.BuildPartC();
                    }
                }
            }
        }

        public static void Solve()
        {
            Task("OOP1Creat9");
            Builder Builder1 = new ConcreteBuilder1();
            Builder Builder2 = new ConcreteBuilder2();
            Director Director1 = new Director(Builder1);
            Director Director2 = new Director(Builder2);

            for (int i = 0; i < 5; i++)
            {
                string s = GetString();
                Director1.Construct(s);
                Put(Director1.GetResult());
                Director2.Construct(s);
                Put(Director2.GetResult());
            }
        }
    }
}
