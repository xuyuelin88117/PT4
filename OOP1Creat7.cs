// File: "OOP1Creat7"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Prototype
        {
            public abstract Prototype Clone();
            public abstract void ChangeId(int id);
            public abstract string Getstr();
        }

        // Implement the ConcretePrototype1
        //   and ConcretePrototype2 descendant classes
        public class ConcretePrototype1 : Prototype
        {

            private string data;
            private int id;


            public ConcretePrototype1(string data, int n)
            {
                this.data = data;
                this.id = n;
            }
            public override Prototype Clone()
            {
                return new ConcretePrototype1(data, id);
            }
            public override void ChangeId(int id) { this.id = id; }
            public override string Getstr() { return "CP1=" + data + "=" + Convert.ToString(id); }
        }
        public class ConcretePrototype2 : Prototype
        {

            private string data;
            private int id;

            public ConcretePrototype2(string data, int n)
            {
                this.data = data;
                this.id = n;
            }
            public override Prototype Clone() { return new ConcretePrototype2(data, id); }
            public override void ChangeId(int id) { this.id = id; }
            public override string Getstr() { return "CP2=" + data + "=" + Convert.ToString(id); }
        }

        public class Client
        {
            // Add required fields
            List<Prototype> lp = new List<Prototype>();
            public Client(Prototype p)
            {
                // Implement the constructor
                lp.Add(p);
            }
            public void AddObject(int id)
            {
                // Implement the method
                Prototype p = lp[0].Clone();
                p.ChangeId(id);
                lp.Add(p);
            }
            public string GetObjects()
            {
                //return "";
                // Remove the previous statement and implement the method
                string str = "";
                int i = 0;
                for (; i < lp.Count - 1; i++)
                {
                    str = str + lp[i].Getstr() + " ";
                }
                str = str + lp[i].Getstr();
                return str;
            }
        }

        public static void Solve()
        {
            Task("OOP1Creat7");
            string data = GetString();
            int N = GetInt();
            int[] id = new int[N];
            int i;
            for (i = 0; i < N; i++)
            {
                id[i] = GetInt();
            }
            ConcretePrototype1 cp1 = new ConcretePrototype1(data, id[0]);
            ConcretePrototype2 cp2 = new ConcretePrototype2(data, id[0]);
            Client c1 = new Client(cp1);
            Client c2 = new Client(cp2);
            for (i = 1; i < N; i++)
            {
                c1.AddObject(id[i]);
                c2.AddObject(id[i]);
            }
            Put(c1.GetObjects());
            Put(c2.GetObjects());
        }
    }
}
