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
            public ConcreteDecoratorA(Component comp) { this.comp = comp; }
            public override string Show() { return "==" + Convert.ToString(comp.Show()) + "=="; }
        }
        public class ConcreteDecoratorB : Decorator
        {
            public ConcreteDecoratorB(Component comp) { this.comp = comp; }
            public override string Show() { return "(" + Convert.ToString(comp.Show()) + ")"; }
        }
        public static void Solve()
        {
            Task("OOP2Struc7");
            int n = GetInt();
            var component = new Component[n];
            for (int i = 0; i < n; i++)
            {
                string s1, s2;
                (s1, s2) = GetString2();
                foreach (var c in s2)
                {
                    var obj = new ConcreteComponent(s1);
                    if (c == 'A')
                    {
                        var conA = new ConcreteDecoratorA(obj);
                        s1 = conA.Show();
                    }
                    else
                    {
                        var conB = new ConcreteDecoratorB(obj);
                        s1 = conB.Show();
                    }
                }
                component[i] = new ConcreteComponent(s1);
            }
            foreach (var c in component.Reverse())
            {
                Put(c.Show());
            }
        }
    }
}
