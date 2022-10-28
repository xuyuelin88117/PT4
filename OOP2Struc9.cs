// File: "OOP2Struc9"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Subject
        {
            public abstract string OperationA();
            public abstract string OperationB();
            public abstract string OperationC();
            public abstract string OperationD();
        }

        // Implement the RealSubject and Proxy descendant classes
        public class RealSubject : Subject
        {
            public override string OperationA() { return "A (Real)"; }
            public override string OperationB() { return "B (Real)"; }
            public override string OperationC() { return "C (Real)"; }
            public override string OperationD() { return "D (Real)"; }
            public string Operation(char c)
            {
                if (c == 'A') { return OperationA(); }
                else if (c == 'B') { return OperationB(); }
                else if (c == 'C') { return OperationC(); }
                else { return OperationD(); }
            }
        }
        public class Proxy : Subject
        {
            public Proxy(bool defferred_mode, bool protected_mode)
            {
                this.defferred_mode = defferred_mode;
                this.protected_mode = protected_mode;
                if (!defferred_mode)
                    rsubj = new RealSubject();
                else
                    rsubj = null;
            }

            public override string OperationA()
            {
                if (rsubj != null)
                    return rsubj.OperationA();
                else
                    return "A (Proxy)";
            }

            public override string OperationB()
            {
                if (protected_mode)
                    return "B denied";
                else if (rsubj != null)
                    return rsubj.OperationB();
                else
                    return "B (Proxy)";
            }

            public override string OperationC()
            {
                if (rsubj == null)
                {
                    rsubj = new RealSubject();
                }
                return rsubj.OperationC();
            }

            public override string OperationD()
            {
                if (protected_mode)
                {
                    return "D denied";
                }
                else if (rsubj == null)
                {
                    rsubj = new RealSubject();
                }
                return rsubj.OperationD();
            }

            public string Operation(char c)
            {
                if (c == 'A')
                {
                    return OperationA();
                }
                else if (c == 'B')
                {
                    return OperationB();
                }
                else if (c == 'C')
                {
                    return OperationC();
                }
                else
                {
                    return OperationD();
                }
            }
            private bool defferred_mode, protected_mode;
            private RealSubject rsubj;
        }

        public static void Solve()
        {
            Task("OOP2Struc9");
            int[] v = new int[3];
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = GetInt();
            }
            string s = GetString();
            int[,] V = { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } };
            for (int i = 0; i < 3; i++)
            {
                if (v[i] == -1)
                {
                    var r = new RealSubject();
                    foreach (var j in s)
                        Put(r.Operation(j));
                }
                else
                {
                    bool a = V[v[i], 0] == 1, b = V[v[i], 1] == 1;
                    var p = new Proxy(a, b);
                    foreach (var j in s)
                        Put(p.Operation(j));
                }
            }
        }
    }
}
