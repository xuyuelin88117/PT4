// File: "OOP3Behav8"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        // Implement the Client, ReceiverA and ReceiverB classes
        public class Client
        {
            public Client() => info = default;
            public void AddLeft(string newInfo) { info = newInfo + info; }
            public void AddRight(string newInfo) { info += newInfo; }
            public string GetInfo() { return info; }

            private string info;
        }

        public class ReceiverA
        {
            public ReceiverA(Client cl, string i)
            {
                this.cl = cl;
                this.i = i;
            }
            public void ActionA() { cl.AddLeft(i); }

            private Client cl;
            private string i;
        }

        public class ReceiverB
        {
            public ReceiverB(Client cl, string i)
            {
                this.cl = cl;
                this.i = i;
            }
            public void ActionB() { cl.AddRight(i); }

            private Client cl;
            private string i;
        }
        public abstract class Command
        {
            public abstract void Execute();
        }

        // Implement the ConcreteCommandA
        //   and ConcreteCommandB descendant classes
        public class ConcreteCommandA : Command
        {
            public ConcreteCommandA(ReceiverA r) => this.r = r;
            public override void Execute() => r.ActionA();

            private ReceiverA r;
        }

        public class ConcreteCommandB : Command
        {
            public ConcreteCommandB(ReceiverB r) => this.r = r;
            public override void Execute() => r.ActionB();

            private ReceiverB r;
        }
        public class Invoker
        {
            Command cmd;
            public Invoker(Command cmd)
            {
                this.cmd = cmd;
            }
            public void Invoke()
            {
                cmd.Execute();
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav8");
            int n = GetInt();

            Client cl = new Client();
            var com = new Command[n];
            var inf = new string[n];
            for (int i = 0; i < n; i++)
            {
                inf[i] = GetString();
                if (inf[i][0] == 'A')
                {
                    com[i] = new ConcreteCommandA(new ReceiverA(cl, inf[i]));
                }
                else
                {
                    com[i] = new ConcreteCommandB(new ReceiverB(cl, inf[i]));
                }
            }
            
            int k = GetInt();
            var inv = new Invoker[k];
            for (var i = 0; i < k; i++)
            {
                inv[i] = new Invoker(com[GetInt()]);
            }

            int m = GetInt();
            for (var i = 0; i < m; i++)
            {
                inv[GetInt()].Invoke();
                Put(cl.GetInfo());
            }
        }
    }
}
