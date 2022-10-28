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
            public override string OperationA() => "A (Real)";
            public override string OperationB() => "B (Real)";
            public override string OperationC() => "C (Real)";
            public override string OperationD() => "D (Real)";

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
        }

        public class Proxy : Subject
        {
            public Proxy(bool defferredMode, bool protecteMode)
            {
                this.defferredMode = defferredMode;
                this.protecteMode = protecteMode;
                rsubj = defferredMode ? null : new RealSubject();
            }

            public override string OperationA() => rsubj?.OperationA() ?? "A (Proxy)";

            public override string OperationB()
                => protecteMode ? "B denied" : rsubj?.OperationB() ?? "B (Proxy)";

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
                if (protecteMode)
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

            private bool defferredMode, protecteMode;
            private RealSubject rsubj;
        }

        public static void Solve()
        {
            Task("OOP2Struc9");

            int[] mode = new int[3];
            for (int i = 0; i < mode.Length; i++)
            {
                mode[i] = GetInt();
            }
            string s = GetString();

            int[,] type = { { 0, 0 }, { 1, 0 }, { 0, 1 }, { 1, 1 } };
            for (int i = 0; i < 3; i++)
            {
                if (mode[i] == -1)
                {
                    var r = new RealSubject();
                    foreach (var c in s)
                    {
                        Put(r.Operation(c));
                    }
                }
                else
                {
                    bool a = type[mode[i], 0] == 1, b = type[mode[i], 1] == 1;
                    var p = new Proxy(a, b);
                    foreach (var c in s)
                    {
                        Put(p.Operation(c));
                    }
                }
            }
        }
    }
}
