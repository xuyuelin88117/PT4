// File: "OOP3Behav10"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class State
        {
            public abstract string GetNextToken();
        }
        // Implement the ConcreteStateNormal, ConcreteStateString,
        public class ConcreteStateNormal : State
        {
            public int index;
            public Context ct;
            public ConcreteStateNormal(Context ct, int index)
            {
                this.ct = ct;
                this.index = index;
            }
            public override string GetNextToken()
            {
                string str = "Normal:";
                while (true)
                {
                    char c = ct.GetCharAt(index++);

                    if (c == '{')
                    {
                        ct.SetState(new ConcreteStateComm(ct, index));
                        break;
                    }
                    else if (c == '"')
                    {
                        ct.SetState(new ConcreteStateString(ct, index));
                        break;
                    }
                    else if (c == '.')
                    {
                        ct.SetState(new ConcreteStateFinal());
                        break;
                    }
                    else
                        str = str + c;
                }
                return str;
            }
        }
        public class ConcreteStateString : State
        {
            public int index;
            public Context ct;
            public ConcreteStateString(Context ct, int index)
            {
                this.ct = ct;
                this.index = index;
            }
            public override string GetNextToken()
            {
                string s = "";
                string str = "String:";
                char c1, c2;
                while (true)
                {
                    c1 = ct.GetCharAt(index++);
                    c2 = ct.GetCharAt(index);
                    if (c1 == '"' && c2 == '"')
                    {
                        s = s + c1;
                        index++;
                    }
                    else if (c1 == '"')
                    {
                        ct.SetState(new ConcreteStateNormal(ct, index));
                        break;
                    }
                    else if (c1 == '.')
                    {
                        str = "ErrString:";
                        ct.SetState(new ConcreteStateFinal());
                        break;
                    }
                    else
                    {
                        s = s + c1;
                    }
                }
                return str + s;
            }
        }
        //   ConcreteStateComm and ConcreteStateFin descendant classes
        public class ConcreteStateComm : State
        {
            public int index;
            public Context ct;
            public ConcreteStateComm(Context context, int index)
            {
                ct = context;
                this.index = index;
            }
            public override string GetNextToken()
            {
                string s1 = "";
                string s2 = "Comm:";
                char ch;
                while (true)
                {
                    ch = ct.GetCharAt(index++);
                    if (ch == '}')
                    {
                        ct.SetState(new ConcreteStateNormal(ct, index));
                        break;
                    }
                    else if (ch == '.')
                    {
                        s2 = "ErrComm:";
                        ct.SetState(new ConcreteStateFinal());
                        break;
                    }
                    else
                    {
                        s1 = s1 + ch;
                    }
                }
                return s2 + s1;
            }
        }
        public class ConcreteStateFinal : State
        {
            public ConcreteStateFinal() { }
            public override string GetNextToken() { return ""; }
        }
        // Implement the Context class
        public class Context
        {
            public string str;
            public State currentState;
            public Context(string text)
            {
                this.str = text;
                this.currentState = new ConcreteStateNormal(this, 0);
            }
            public char GetCharAt(int index)
            {
                return str[index];
            }
            public void SetState(State newState)
            {
                currentState = newState;
            }
            public string GetNextToken()
            {
                return currentState.GetNextToken();
            }
        }
        public static void Solve()
        {
            Task("OOP3Behav10");
            string str = GetString();

            Context con = new Context(str);

            while ((str = con.GetNextToken()) != "")
            {
                Put(str);
            }
        }
    }
}
