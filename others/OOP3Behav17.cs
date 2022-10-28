// File: "OOP3Behav17"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public class Context
        {
            // Add the constructor, required fields and methods
            string[] Name = new string[10];
            int[] Value = new int[10];
            public Context()
            {
                for (int i = 0; i < 10; i++)
                {
                    Name[i] = ((char)('a' + i)).ToString();
                    Value[i] = 0;
                }
            }
            public void SetVar(int ind, string name, int value)
            {
                Name[ind] = name;
                Value[ind] = value;
            }
            public string GetName(int ind) { return Name[ind]; }
            public int GetValue(int ind) { return Value[ind]; }
        }

        public abstract class AbstractExpression
        {
            public abstract string InterpretA(Context cont);
            public abstract string InterpretB(Context cont);
        }

        // Implement the TermStr, NontermConcat, NontermIf
        //   and NontermLoop descendant classes

        class TermStr : AbstractExpression
        {
            string s;
            public TermStr(string S) { s = S; }
            public override string InterpretA(Context cont) { return s; }
            public override string InterpretB(Context cont) { return s; }
        }
        class NontermConcat : AbstractExpression
        {
            List<AbstractExpression> exprs = new List<AbstractExpression>();
            public NontermConcat(List<AbstractExpression> V) { exprs = V; }
            override public string InterpretA(Context cont)
            {
                string s = "";
                foreach (var e in exprs)
                    s = s + e.InterpretA(cont);
                return s;
            }
            override public string InterpretB(Context cont)
            {
                string s = "";
                foreach (var e in exprs)
                    s = s + e.InterpretB(cont);
                return s;
            }
        }
        class NontermIf : AbstractExpression
        {
            AbstractExpression expr1, expr2;
            int ind;
            public NontermIf(AbstractExpression Expr1, AbstractExpression Expr2, int Ind) { expr1 = Expr1; expr2 = Expr2; ind = Ind; }
            override public string InterpretA(Context cont)
            {
                return "(" + cont.GetName(ind) + "?" + expr1.InterpretA(cont) + ":" + expr2.InterpretA(cont) + ")";
            }
            override public string InterpretB(Context cont)
            {
                if (cont.GetValue(ind) != 0)
                    return expr1.InterpretB(cont);
                else return expr2.InterpretB(cont);
            }
        }
        class NontermLoop : AbstractExpression
        {
            AbstractExpression expr;
            int ind;
            public NontermLoop(AbstractExpression Expr, int Ind) { expr = Expr; ind = Ind; }
            override public string InterpretA(Context cont)
            {
                return "(" + cont.GetName(ind) + ":" + expr.InterpretA(cont) + ")";
            }
            override public string InterpretB(Context cont)
            {
                if (cont.GetValue(ind) > 0)
                {
                    string s = "";
                    for (int i = 0; i < cont.GetValue(ind); i++)
                        s = s + expr.InterpretB(cont);
                    return s;
                }
                else return "";
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav17");
            int N = GetInt();
            List<AbstractExpression> nodes = new List<AbstractExpression>();
            for (int i = 0; i < N; i++)
            {
                char c = GetChar();
                if (c == 'C')
                {
                    int K = GetInt();
                    List<AbstractExpression> expr = new List<AbstractExpression>();
                    for (int j = 0; j < K; j++)
                    {
                        int n = GetInt();
                        expr.Add(nodes[n]);
                    }
                    AbstractExpression t = new NontermConcat(expr);
                    nodes.Add(t);
                }
                if (c == 'I')
                {
                    int n = GetInt();
                    int n1 = GetInt(), n2 = GetInt();
                    AbstractExpression t = new NontermIf(nodes[n1], nodes[n2], n);
                    nodes.Add(t);
                }
                if (c == 'L')
                {
                    int n = GetInt();
                    int n1 = GetInt();
                    AbstractExpression t = new NontermLoop(nodes[n1], n);
                    nodes.Add(t);
                }
                if (c == 'S')
                {
                    AbstractExpression ae = new TermStr(GetString());
                    nodes.Add(ae);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int M = GetInt();
                Context ct = new Context();
                for (int j = 0; j < M; j++)
                {
                    ct.SetVar(GetInt(), GetString(), GetInt());
                }
                Put(nodes[nodes.Count - 1].InterpretA(ct));
                Put(nodes[nodes.Count - 1].InterpretB(ct));
            }
        }
    }
}
