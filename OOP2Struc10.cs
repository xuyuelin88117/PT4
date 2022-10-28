// File: "OOP2Struc10"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Implementor
        {
            public abstract string DrawLine(int size);
            public abstract string DrawText(string text);
        }

        // Implement the ConcreteImplementorA
        //   and ConcreteImplementorB descendant classes
        public class ConcreteImplementorA : Implementor
        {
            public int num;
            public string s;
            public ConcreteImplementorA(int size, string text)
            {
                num = size;
                s = text;
            }
            public override string DrawLine(int size)
            {
                string s1 = null;
                for (int i = 0; i < num; i++)
                {
                    s1 = s1 + '-';
                }
                return s1;
            }
            public override string DrawText(string text)
            {
                // transform(s.begin(), s.end(), s.begin(), ::tolower);
                return s.ToLower();
            }
        }

        public class ConcreteImplementorB : Implementor
        {
            public int num;
            public string s;
            public ConcreteImplementorB(int size, string text)
            {
                num = size;
                s = text;
            }
            public override string DrawLine(int size)
            {
                string s1 = null;
                for (int i = 0; i < num; i++)
                {
                    s1 = s1 + '=';
                }
                return s1;
            }
            public override string DrawText(string text)
            {
                // transform(s.begin(), s.end(), s.begin(), ::toupper);
                return s.ToUpper();
            }
        }
        public class Abstraction
        {
            protected int size;
            protected Implementor imp;
            public Abstraction(Implementor imp, int size)
            {
                this.imp = imp;
                this.size = size;
            }
            // Complete the implementation of the class
            public virtual string ShowA()
            {
                return imp.DrawLine(size);
            }
            public virtual string ShowB()
            {
                return imp.DrawLine(size);
            }
            public virtual void setsize(int n)
            {
                size = n;
            }
        }

        // Implement the RefinedAbstraction descendant class
        public class RefinedAbstraction : Abstraction
        {
            public string text;
            public RefinedAbstraction(Implementor imp, int size, string text) : base(imp, size) { }
            public override string ShowA()
            {
                string s = null;
                s = '-' + imp.DrawText(text);
                if (s.Length >= size)
                {
                    s = s.Substring(0, size);
                }
                else
                {
                    int k = size - s.Length;
                    for (int i = 0; i < k; i++)
                    {
                        s = s + '-';
                        ShowLine(s);
                        ShowLine(size - s.Length);
                    }
                }

                return s;
            }
            public override string ShowB()
            {
                string s;
                s = '=' + imp.DrawText(text);
                if (s.Length >= size)
                {
                    s = s.Substring(0, size);
                }
                else
                {
                    int k = size - s.Length;
                    for (int i = 0; i < k; i++)
                    {
                        s = s + '=';
                    }
                }
                // transform(s.begin(), s.end(), s.begin(), ::toupper);
                return s.ToUpper();
            }
        }
        public static void Solve()
        {
            Task("OOP2Struc10");
            int size = GetInt();
            string s = GetString();
            Implementor IMP_1 = new ConcreteImplementorA(size, s);
            Implementor IMP_2 = new ConcreteImplementorB(size, s);
            Abstraction ABS = new RefinedAbstraction(IMP_1, size, s);
            ABS.setsize(size);

            Put(IMP_1.DrawLine(size), IMP_2.DrawLine(size), ABS.ShowA(), ABS.ShowB());

            for (int i = 0; i < 5; i++)
            {
                int num = GetInt();
                Implementor Test_IMP_1 = new ConcreteImplementorA(num, s);
                Implementor TEST_IMP_2 = new ConcreteImplementorB(num, s);
                Abstraction TEST_ABS = new RefinedAbstraction(Test_IMP_1, size, s);
                TEST_ABS.setsize(num);
                Put(Test_IMP_1.DrawLine(num), TEST_IMP_2.DrawLine(num), TEST_ABS.ShowA(), TEST_ABS.ShowB());
            }
        }
    }
}
