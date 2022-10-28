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
            public virtual void BuildStart() {}
            public virtual void BuildPartA() {}
            public virtual void BuildPartB() {}
            public virtual void BuildPartC() {}
            public abstract string GetResult();
        }

        // Implement the ConcreteBuilder1
        //   and ConcreteBuilder2 descendant classes
        public class ConcreteBuilder1 : Builder
        {
            public ConcreteBuilder1()
            {
                this.info = "";
            }
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
            string info;
        }
        public class ConcreteBuilder2 : Builder
        {
            public ConcreteBuilder2()
            {
                this.info = "";
            }
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
            string info;
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
                for(i = 0; i < templat.Length; i++)
                {
                    if(templat[i] == 'A')
                    {
                        b.BuildPartA();
                    }
                    if(templat[i] == 'B')
                    {
                        b.BuildPartB();
                    }
                    if(templat[i] == 'C')
                    {
                        b.BuildPartC();
                    } 
                }
            }
        }

        public static void Solve()
        {
            Task("OOP1Creat9");
            Builder bd1 = new ConcreteBuilder1();
            Director dr1 = new Director(bd1);
            Builder bd2 = new ConcreteBuilder2();
            Director dr2 = new Director(bd2);
            for(int i = 0; i < 5; i++)
            {
                string str = GetString();
                dr1.Construct(str);
                Put(dr1.GetResult());
                dr2.Construct(str);
                Put(dr2.GetResult());
            }
        }
    }
}
