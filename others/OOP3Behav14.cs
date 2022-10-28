// File: "OOP3Behav14"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Request
        {
            public abstract string GetParam();
            public abstract string ToStr();
        }

        // Implement the RequestA and RequestB descendant classes

        public class RequestA : Request
        {
            public int param;
            public RequestA(int param) 
            {
                this.param = param;
            }
            public override string GetParam() 
            {
                return param.ToString();
            }
            public override string ToStr()
            {
                return "A:" + param;
            }
        }

        public class RequestB : Request
        {
            public string param;
            public RequestB(string param)
            {
                this.param = param;
            }

            public override string GetParam()
            {
                return param;
            }

            public override string ToStr()
            {
                return "B:" + param;
            }
        }

        public class Handler
        {
            Handler successor;
            public Handler(Handler successor)
            {
                this.successor = successor;
            }
            public virtual void HandleRequest(Request req)
            {
                // Implement the method
                if (successor != null)
                    successor.HandleRequest(req);
                else
                    Put("Request " + req.ToStr() + " not processed");
            }
        }

        // Implement the HandlerA and HandlerB descendant classes

        public class HandlerA : Handler
        {
            int id, param1, param2;

            public HandlerA(Handler successor, int id, int param1, int param2) : base(successor)
            {
                this.id = id;
                this.param1 = param1;
                this.param2 = param2;
            }
            public override void HandleRequest(Request req)
            {
                if (req.ToStr()[0] == 'A')
                    if (Convert.ToInt32(req.GetParam()) >= param1 && Convert.ToInt32(req.GetParam()) <= param2)
                    {
                        Put("Request " + req.ToStr() + " processed by handler " + id);
                        return;
                    }
                base.HandleRequest(req);
            }
        }

        public class HandlerB : Handler
        {
            int id;
            string param1, param2;

            public HandlerB(Handler successor, int id, string param1, string param2) : base(successor)
            {
                this.id = id;
                this.param1 = param1;
                this.param2 = param2;
            }
            public override void HandleRequest(Request req)
            {
                if (req.ToStr()[0] == 'B')
                {
                    if (string.CompareOrdinal(req.GetParam(), param1) >= 0 && string.CompareOrdinal(req.GetParam(), param2) <= 0)
                    {
                        Put("Request " + req.ToStr() + " processed by handler " + id);
                        return;
                    }
                }
                base.HandleRequest(req);
            }
        }

        public class Client
        {
            Handler h;
            public Client(Handler h)
            {
                this.h = h;
            }
            public void SendRequest(Request req)
            {
                h.HandleRequest(req);
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav14");
            int N = GetInt();
            Handler h = new Handler(null);
            for (int i = 0; i < N; i++) 
            {
                char ch1 = GetChar();
                if (ch1 == 'A')
                {
                    int par1 = GetInt();
                    int par2 = GetInt();
                    h = new HandlerA(h, i, par1, par2);
                }
                else if (ch1 == 'B')
                {
                    string par1 = GetString();
                    string par2 = GetString();
                    h = new HandlerB(h, i, par1, par2);
                }
            }
            Client cli = new Client(h);
            int K = GetInt();
            for (int i = 0; i < K; i++) 
            {
                char ch = GetChar();
                Request request;
                if (ch == 'A')
                {
                    int par = GetInt();
                    request = new RequestA(par);
                    cli.SendRequest(request);
                }
                else if (ch == 'B')
                {
                    string par = GetString();
                    request = new RequestB(par);
                    cli.SendRequest(request);
                }
            }
        }
    }
}
