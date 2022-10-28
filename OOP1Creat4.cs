// File: "OOP1Creat4"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class AbstractProductA
        {
            public abstract void A();
            public abstract string GetInfo();
        }

        // Implement the ProductA1 and ProductA2 descendant classes
        public class ProductA1 : AbstractProductA
        {

            public ProductA1(int i) { info = Convert.ToString(i); }
            public override void A() { int n = Convert.ToInt32(info); info = Convert.ToString(2 * n); }
            public override string GetInfo() { return info; }
            private string info;
        }

        public class ProductA2 : AbstractProductA
        {

            public ProductA2(int i) { info = Convert.ToString(i); }
            public override void A() { info += info; }
            public override string GetInfo() { return info; }
            private string info;
        }
        public abstract class AbstractProductB
        {
            public abstract void B(AbstractProductA objA);
            public abstract string GetInfo();
        }

        // Implement the ProductB1 and ProductB2 descendant classes
        public class ProductB1 : AbstractProductB
        {
            public ProductB1(int i) { info = Convert.ToString(i); }
            public override void B(AbstractProductA objA) { info = Convert.ToString(Convert.ToInt32(info) + Convert.ToInt32(objA.GetInfo())); }
            public override string GetInfo() { return info; }
            private string info;
        }
        
        public class ProductB2 : AbstractProductB
        {
            public ProductB2(int i) { info = Convert.ToString(i); }
            public override void B(AbstractProductA objA) { info += objA.GetInfo(); }
            public override string GetInfo() { return info; }
            private string info;
        }
        public abstract class AbstractFactory
        {
            public abstract AbstractProductA CreateProductA(int info);
            public abstract AbstractProductB CreateProductB(int info);
        }

        // Implement the ConcreteFactory1
        //   and ConcreteFactory2 descendant classes
        public class Factory1 : AbstractFactory
        {
            public override AbstractProductA CreateProductA(int info) { return new ProductA1(info); }
            public override AbstractProductB CreateProductB(int info) { return new ProductB1(info); }
        }

        public class Factory2 : AbstractFactory
        {
            public override AbstractProductA CreateProductA(int info) { return new ProductA2(info); }
            public override AbstractProductB CreateProductB(int info) { return new ProductB2(info); }
        }
        public AbstractFactory Factory(int nf)
        {
            if (nf == 1)
                return new Factory1();
            else
                return new Factory2();
        }
        public static void Solve()
        {
            Task("OOP1Creat4");
            int nf, na, nb;
            (nf, na, nb) = GetInt3();
            string s = GetString();

            AbstractFactory f = null;//Factory(nf);
            if (nf == 1)
                f = new Factory1();
            else
                f = new Factory2();
            AbstractProductA pa = f.CreateProductA(na);
            AbstractProductB pb = f.CreateProductB(nb);

            foreach (char c in s)
            {
                if (c == 'A') { pa.A(); }
                else { pb.B(pa); }
            }
            Put(pa.GetInfo(), pb.GetInfo());

        }
    }
}
