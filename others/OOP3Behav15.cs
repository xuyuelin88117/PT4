// File: "OOP3Behav15"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Element
        {
            public abstract void Accept(Visitor v);
        }

        public class ConcreteElementA : Element
        {
            // Add required fields and methods
            int data;
            public ConcreteElementA(int Data) { data = Data; }
            public int GetData() { return data; }
            public void SetData(int newData) { data = newData; }
            public override void Accept(Visitor v)
            {
                // Implement the method
                v.VisitConcreteElementA(this);
            }
        }

        public class ConcreteElementB : Element
        {
            // Add required fields and methods
            string data;
            public ConcreteElementB(string Data) { data = Data; }
            public string GetData() { return data; }
            public void SetData(string newData) { data = newData; }
            public override void Accept(Visitor v)
            {
                // Implement the method
                v.VisitConcreteElementB(this);
            }
        }

        public class ConcreteElementC : Element
        {
            // Add required fields and methods
            float data;
            public ConcreteElementC(float Data)  { data = Data; }
            public float GetData() { return data; }
            public void SetData(float newData) { data = newData; }
            public override void Accept(Visitor v)
            {
                // Implement the method
                v.VisitConcreteElementC(this);
            }
        }

        public class ObjectStructure
        {
            Element[] struc;
            public ObjectStructure(Element[] Struc)
            {
                // Implement the constructor
                struc = Struc;
            }
            public void Accept(Visitor v)
            {
                foreach (var e in struc)
                    e.Accept(v);
            }
        }

        public abstract class Visitor
        {
            public abstract void VisitConcreteElementA(ConcreteElementA e);
            public abstract void VisitConcreteElementB(ConcreteElementB e);
            public abstract void VisitConcreteElementC(ConcreteElementC e);
        }

        // Implement the ConcreteVisitor1, ConcreteVisitor2
        //   and ConcreteVisitor3 descendant classes
        public class ConcreteVisitor1 : Visitor
        {
            override public void VisitConcreteElementA(ConcreteElementA e) { Put(e.GetData()); }
            override public void VisitConcreteElementB(ConcreteElementB e) { Put(e.GetData()); }
            override public void VisitConcreteElementC(ConcreteElementC e) { Put(e.GetData()); }
        }
        public class ConcreteVisitor2 : Visitor
        {
            override public void VisitConcreteElementA(ConcreteElementA e) { e.SetData(0 - e.GetData()); }
            override public void VisitConcreteElementB(ConcreteElementB e)
            {
                string s = e.GetData();
                string s1 = "";
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    s1 += s[i];
                }
                e.SetData(s1);
            }
            override public void VisitConcreteElementC(ConcreteElementC e) { e.SetData(1 / e.GetData()); }
        }

        public class ConcreteVisitor3 : Visitor
        {
            public int resultA = 0;
            public string resultB = "";
            public float resultC = 1;
            public ConcreteVisitor3() { }
            override public void VisitConcreteElementA(ConcreteElementA e) { resultA = resultA + (e.GetData()); }
            override public void VisitConcreteElementB(ConcreteElementB e) { resultB = resultB + (e.GetData()); }
            override public void VisitConcreteElementC(ConcreteElementC e) { resultC = resultC * (e.GetData()); }
            public int GetResultA() { return resultA; }
            public string GetResultB() { return resultB; }
            public float GetResultC() { return resultC; }
        }
        
        public static void Solve()
        {
            Task("OOP3Behav15");
            int N=GetInt();
            Element [] V=new Element [N];
            for (int i = 0; i < N; i++)
            {
                char c1=GetChar();
                if (c1 == 'A')
                {
                    int data=GetInt();
                    ConcreteElementA a = new ConcreteElementA(data);
                    V[i] = a;
                }
                else
                {
                    if (c1 == 'B')
                    {
                        string data=GetString();
                        ConcreteElementB b = new ConcreteElementB(data);
                        V[i]=b;
                    }
                    else
                    {
                        float data = (float)GetDouble();
                        ConcreteElementC c = new ConcreteElementC(data);
                        V[i]=c;
                    }
                }
            }
            ObjectStructure struc = new ObjectStructure(V);
            ConcreteVisitor1 v1 = new ConcreteVisitor1();
            ConcreteVisitor2 v2 = new ConcreteVisitor2();
            ConcreteVisitor3 v3 = new ConcreteVisitor3();
            struc.Accept(v1);
            struc.Accept(v2);
            struc.Accept(v1);
            struc.Accept(v3);
            Put(v3.GetResultA());
            Put(v3.GetResultB());
            Put(v3.GetResultC());
        }
    }
}
