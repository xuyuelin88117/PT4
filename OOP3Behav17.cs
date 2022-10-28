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
            string[] str = new string[10];
            int[] num = new int[10];
            public Context()
            {
                for (int i = 0; i < 10; i++)
                {
                    str[i] = ((char)('a' + i)).ToString();
                    num[i] = 0;
                }
            }
            public void SetVar(int i, string s, int count)
            {
                str[i] = s;
                num[i] = count;
            }
            public string GetName(int i) { return str[i]; }
            public int GetValue(int i) { return num[i]; }
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
            public TermStr(string ts) { s = ts; }
            public override string InterpretA(Context cont) { return s; }
            public override string InterpretB(Context cont) { return s; }
        }
        class NontermConcat : AbstractExpression
        {
            List<AbstractExpression> ae = new List<AbstractExpression>();
            public NontermConcat(List<AbstractExpression> l) { ae = l; }
            override public string InterpretA(Context cont)
            {
                string s = "";
                foreach (var i in ae)
                    s = s + i.InterpretA(cont);
                return s;
            }
            override public string InterpretB(Context cont)
            {
                string s = "";
                foreach (var i in ae)
                    s = s + i.InterpretB(cont);
                return s;
            }
        }
        class NontermIf : AbstractExpression
        {
            AbstractExpression ae1, ae2;
            int i;
            public NontermIf(AbstractExpression e1, AbstractExpression e2, int num) { ae1 = e1; ae2 = e2; i = num; }
            override public string InterpretA(Context cont)
            {
                return "(" + cont.GetName(i) + "?" + ae1.InterpretA(cont) + ":" + ae2.InterpretA(cont) + ")";
            }
            override public string InterpretB(Context cont)
            {
                if (cont.GetValue(i) != 0)
                    return ae1.InterpretB(cont);
                else return ae2.InterpretB(cont);
            }
        }
        class NontermLoop : AbstractExpression
        {
            AbstractExpression ae;
            int i;
            public NontermLoop(AbstractExpression ex, int num) { ae = ex; i = num; }
            override public string InterpretA(Context cont)
            {
                return "(" + cont.GetName(i) + ":" + ae.InterpretA(cont) + ")";
            }
            override public string InterpretB(Context cont)
            {
                if (cont.GetValue(i) > 0)
                {
                    string s = "";
                    for (int i = 0; i < cont.GetValue(this.i); i++)
                        s = s + ae.InterpretB(cont);
                    return s;
                }
                else return "";
            }
        }
        public static void Solve()
        {
            Task("OOP3Behav17");
            int num = GetInt();
            List<AbstractExpression> ae = new List<AbstractExpression>();
            for (int i = 0; i < num; i++)
            {
                char c = GetChar();
                if (c == 'C')
                {
                    int k = GetInt();
                    List<AbstractExpression> ex = new List<AbstractExpression>();
                    for (int j = 0; j < k; j++)
                    {
                        int n = GetInt();
                        ex.Add(ae[n]);
                    }
                    AbstractExpression t = new NontermConcat(ex);
                    ae.Add(t);
                }
                if (c == 'I')
                {
                    int n = GetInt();
                    int n1 = GetInt(), n2 = GetInt();
                    AbstractExpression a = new NontermIf(ae[n1], ae[n2], n);
                    ae.Add(a);
                }
                if (c == 'L')
                {
                    int n1 = GetInt();
                    int n2 = GetInt();
                    AbstractExpression a = new NontermLoop(ae[n2], n1);
                    ae.Add(a);
                }
                if (c == 'S')
                {
                    AbstractExpression a = new TermStr(GetString());
                    ae.Add(a);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int nn = GetInt();
                Context con = new Context();
                for (int j = 0; j < nn; j++)
                {
                    con.SetVar(GetInt(), GetString(), GetInt());
                }
                Put(ae[ae.Count - 1].InterpretA(con));
                Put(ae[ae.Count - 1].InterpretB(con));
            }
        }
    }
}
