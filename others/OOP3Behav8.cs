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

        public class Client
        {
            public Client() => info = default;
            public void AddLeft(string newInfo) => info = newInfo + info;
            public void AddRight(string newInfo) => info += newInfo;
            public string GetInfo() => info;

            private string info;
        }

        public class ReceiverA
        {
            public ReceiverA(Client cli, string info)
            {
                this.cli = cli;
                this.info = info;
            }

            public void ActionA() => cli.AddLeft(info);

            private Client cli;
            private string info;
        }

        public class ReceiverB
        {
            public ReceiverB(Client cli, string info)
            {
                this.cli = cli;
                this.info = info;
            }

            public void ActionB() => cli.AddRight(info);

            private Client cli;
            private string info;
        }

        public abstract class Command
        {
            public abstract void Execute();
        }


        public class ConcreteCommandA : Command
        {
            public ConcreteCommandA(ReceiverA recv) => this.recv = recv;
            public override void Execute() => recv.ActionA();

            private ReceiverA recv;
        }

        public class ConcreteCommandB : Command
        {
            public ConcreteCommandB(ReceiverB recv) => this.recv = recv;
            public override void Execute() => recv.ActionB();

            private ReceiverB recv;
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

            Client cli = new Client();
            var cmd = new Command[n];
            var infos = new string[n];
            for (int i = 0; i < n; i++)
            {
                infos[i] = GetString();
                if (infos[i][0] == 'A')
                {
                    cmd[i] = new ConcreteCommandA(new ReceiverA(cli, infos[i]));
                }
                else
                {
                    cmd[i] = new ConcreteCommandB(new ReceiverB(cli, infos[i]));
                }
            }

            int k = GetInt();
            var inv = new Invoker[k];
            for (var i = 0; i < k; i++)
            {
                int kn = GetInt();
                inv[i] = new Invoker(cmd[kn]);
            }

            int m = GetInt();
            for (var i = 0; i < m; i++)
            {
                int mn = GetInt();
                inv[mn].Invoke();
                Put(cli.GetInfo());
            }
        }
    }
}
