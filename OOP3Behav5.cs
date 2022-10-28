// File: "OOP3Behav5"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class AbstractClass
        {
            public abstract string PrimitiveOperation();
            // Implement methods TemplateMethod,
            // BasicOperation1, BasicOperation2 and HookOperation
            public string TemplateMethod() { return BasicOperation1() + PrimitiveOperation() + BasicOperation2() + HookOperation(); }
            public string BasicOperation1() { return "Boil water"; }
            public string BasicOperation2() { return "=Pour into a cup"; }
            protected virtual string HookOperation() { return ""; }
        }

        // Implement the ConcreteClass1, ConcreteClass2, ConcreteClass3
        public class ConcreteClass1 : AbstractClass
        {
            public override string PrimitiveOperation() { return "=Brew tea"; }
        }

        public class ConcreteClass2 : AbstractClass
        {
            public override string PrimitiveOperation()
            { return "=Brew coffee"; }
        }

        public class ConcreteClass3 : AbstractClass
        {
            public ConcreteClass3() { }
            override public string PrimitiveOperation()
            {
                return "=Brew tea";
            }
            override protected string HookOperation()
            {
                return "=Add sugar and lemon";
            }
        }
        //   and ConcreteClass4 descendant classes
        public class ConcreteClass4 : AbstractClass
        {
            public ConcreteClass4() { }
            override public string PrimitiveOperation()
            {
                return "=Brew coffee";
            }
            override protected string HookOperation()
            {
                return "=Add sugar and milk";
            }
        }
        
        public static void Solve()
        {
            Task("OOP3Behav5");
            int n = GetInt();
            int[] a = new int[n];
            AbstractClass[] A = new AbstractClass[n];
            for (int i = 0; i < n; i++)
            {
                a[i] = GetInt();
                switch (a[i])
                {
                    case 1:
                        A[i] = new ConcreteClass1();
                        break;
                    case 2:
                        A[i] = new ConcreteClass2();
                        break;
                    case 3:
                        A[i] = new ConcreteClass3();
                        break;
                    case 4:
                        A[i] = new ConcreteClass4();
                        break;
                }

            }
            for (int i = n - 1; i >= 0; i--)
            {
                Put(A[i].TemplateMethod());
            }
        }
    }
}
