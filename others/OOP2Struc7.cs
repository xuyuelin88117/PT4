// File: "OOP2Struc7"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Component
        {
            public abstract string Show();
        }

        // Implement the ConcreteComponent descendant class
        public class ConcreteComponent : Component
        {
            public ConcreteComponent(string s) => text = s;
            public override string Show() => text;

            private string text;
        }

        public abstract class Decorator : Component
        {
            protected Component comp;
        }

        // Implement the ConcreteDecoratorA
        //   and ConcreteDecoratorB descendant classes
        public class ConcreteDecoratorA : Decorator
        {
            public ConcreteDecoratorA(Component comp) => this.comp = comp;
            public override string Show() => $"=={comp.Show()}==";
        }

        public class ConcreteDecoratorB : Decorator
        {
            public ConcreteDecoratorB(Component comp) => this.comp = comp;
            public override string Show() => $"({comp.Show()})";
        }

        public static void Solve()
        {
            Task("OOP2Struc7");

            int n = GetInt();
            var components = new Component[n];
            for (int i = 0; i < n; i++)
            {
                string s = GetString();
                string d = GetString();
                foreach (var c in d)
                {
                    var obj = new ConcreteComponent(s);
                    if (c == 'A')
                    {
                        var conA = new ConcreteDecoratorA(obj);
                        s = conA.Show();
                    }
                    else
                    {
                        var conB = new ConcreteDecoratorB(obj);
                        s = conB.Show();
                    }
                }
                components[i] = new ConcreteComponent(s);
            }

            foreach (var c in components.Reverse())
            {
                Put(c.Show());
            }
        }
    }
}
