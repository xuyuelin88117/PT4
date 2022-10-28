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
            public abstract string ToStr();
            public abstract string GetParam();
        }

        // Implement the RequestA and RequestB descendant classes
        public class RequestA : Request
        {
            public int p;
            public RequestA(int p)
            {
                this.p = p;
            }
            public override string GetParam()
            {
                return p.ToString();
            }
            public override string ToStr()
            {
                return "A:" + p;
            }
        }

        public class RequestB : Request
        {
            public string p;
            public RequestB(string p)
            {
                this.p = p;
            }

            public override string GetParam()
            {
                return p;
            }

            public override string ToStr()
            {
                return "B:" + p;
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
            int idnt, p1, p2;

            public HandlerA(Handler successor, int id, int p1, int p2) : base(successor)
            {
                this.idnt = id;
                this.p1 = p1;
                this.p2 = p2;
            }
            public override void HandleRequest(Request r)
            {
                if (r.ToStr()[0] == 'A')
                    if (Convert.ToInt32(r.GetParam()) >= p1 && Convert.ToInt32(r.GetParam()) <= p2)
                    {
                        Put("Request " + r.ToStr() + " processed by handler " + idnt);
                        return;
                    }
                base.HandleRequest(r);
            }
        }

        public class HandlerB : Handler
        {
            int id;
            string p1, p2;

            public HandlerB(Handler successor, int id, string p1, string p2) : base(successor)
            {
                this.id = id;
                this.p1 = p1;
                this.p2 = p2;
            }
            public override void HandleRequest(Request req)
            {
                if (req.ToStr()[0] == 'B')
                {
                    if (string.CompareOrdinal(req.GetParam(), p1) >= 0 && string.CompareOrdinal(req.GetParam(), p2) <= 0)
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
            int n = GetInt();
            Handler h = new Handler(null);
            for (int i = 0; i < n; i++)
            {
                char c = GetChar();
                if (c == 'A') { h = new HandlerA(h, i, GetInt(), GetInt()); }
                else if (c == 'B') { h = new HandlerB(h, i, GetString(), GetString()); }
            }
            Client cli = new Client(h);
            int k = GetInt();
            for (int i = 0; i < k; i++)
            {
                char c = GetChar();
                if (c == 'A') { cli.SendRequest(new RequestA(GetInt())); }
                else if (c == 'B') { cli.SendRequest(new RequestB(GetString())); }
            }
        }
    }
}
