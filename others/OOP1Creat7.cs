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
            public abstract string GetInfo();
        }

        // Implement the ConcretePrototype1
        //   and ConcretePrototype2 descendant classes
        public class ConcretePrototype1 :Prototype
        {
            private int i;
            private string str;
            public ConcretePrototype1(int i ,string str)
            {
                this.i = i;
                this.str = str;
            }
            public override Prototype Clone()
            {
                ConcretePrototype1 p=new ConcretePrototype1 (i,str);
                return p;
            }
            public override  void ChangeId(int i)
            {
                this.i = i;
            }
            public override string GetInfo()
            {
                return "CP1="+str+"="+Convert.ToString (i);
            }


        }
        public class ConcretePrototype2 :Prototype
        {
            private int i;
            private string str;
            public ConcretePrototype2(int i ,string str)
            {
                this.i = i;
                this.str = str;
            }
            public override  Prototype Clone()
            {
                ConcretePrototype2 p=new ConcretePrototype2 (i,str);
                return p;
            }
            public override void ChangeId(int i)
            {
                this.i = i;
            }
            public override string GetInfo()
            {
                return "CP2="+str+"="+Convert.ToString (i);
            }


        }

        public class Client
        {
            // Add required fields
            List<Prototype> P=new List<Prototype>();
            public Client(Prototype p)
            {
                // Implement the constructor
                P.Add (p);
            }
            public void AddObject(int id)
            {
                // Implement the method
                Prototype p=P[0].Clone ();
                p.ChangeId (id);
                P.Add (p);
            }
            public  string GetObjects()
            {
                //return "";
                string info="";
                int i=0;
                for(;i<P.Count -1;i++){
                    info=info+P[i].GetInfo ()+" ";
                }
                info=info+P[i].GetInfo ();
                return info;
                // Remove the previous statement and implement the method
            }
        }

        public static async void Solve()
        {
            Task("OOP1Creat7");
            string s = GetString();
            int N = GetInt();
            int[]id = new int[N];
            int i;
            for(i = 0;i < N;i++)
            {
                id[i] = GetInt();
            }
            ConcretePrototype1 cp1 = new ConcretePrototype1(id[0],s);
            ConcretePrototype2 cp2 = new ConcretePrototype2(id[0],s);
            Client c1 = new Client(cp1);
            Client c2 = new Client(cp2);
            for(i = 1 ;i < N ; i++)
            {
                c1.AddObject(id[i]);
                c2.AddObject(id[i]);
            }
            Put(c1.GetObjects());
            Put(c2.GetObjects());
        }
    }
}
