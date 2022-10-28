// File: "OOP3Behav3"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class Strategy
        {
            public abstract string Algorithm(string info);
        }

        // Implement the ConcreteStrategyA, ConcreteStrategyB
        //   and ConcreteStrategyC descendant classes
        public class ConcreteStrategyA : Strategy
        {
            public ConcreteStrategyA() { }
            public override string Algorithm(string info)
            {
                info = info + 'A';
                return info;
            }
        }

        public class ConcreteStrategyB : Strategy
        {
            public ConcreteStrategyB() { }
            public override string Algorithm(string info)
            {
                info = info + 'B';
                return info;
            }
        }

        public class ConcreteStrategyC : Strategy
        {
            public ConcreteStrategyC() { }
            public override string Algorithm(string info)
            {
                info = info + 'C';
                return info;
            }
        }

        public abstract class Context
        {
            protected Strategy st;
            public void SetStrategy(Strategy st)
            {
                this.st = st;
            }
            public abstract string RunAlgorithm();
        }

        // Implement the Context1 and Context2 descendant classes

        public class Context1 : Context
        {
            public string s = "1";
            protected Strategy st;
            public override string RunAlgorithm()
            {
                return st.Algorithm(s);
            }
            public Context1(Strategy a)
            {
                this.st = a;
            }
        }

        public class Context2 : Context
        {
            public string s = "2";
            protected Strategy st;
            public override string RunAlgorithm()
            {
                return st.Algorithm(s);
            }
            public Context2(Strategy b)
            {
                this.st = b;
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav3");
            Strategy pStrategyA = new ConcreteStrategyA();
            Strategy pStrategyB = new ConcreteStrategyB();
            Strategy pStrategyC = new ConcreteStrategyC();
            string S;
            int K;
            S = GetString();
            K = GetInt();

            List<Context> V = new List<Context>();
            List<int> L1 = new List<int>();
            for(int i = 0; i < K; i++)
            {
                int b;
                b = GetInt();  
                L1.Add(b);
            }
            for(int i = 0; i < S.Length; i++)
            {
                int flag = 0;
                for(int j = 0; j < L1.Count; j++)
                {
                    if(i == L1[j])
                        flag = 1;
                }
                if(flag == 1)
                {
                    if(S[i] == '1')
                    {
                        Context pcon1 = new Context1(pStrategyC);
                        //pcon1.SetStrategy(pStrategyC);
                        V.Add(pcon1);
                        Put(pcon1.RunAlgorithm());
                    }
                    if(S[i] == '2')
                    {
                        Context pcon2 = new Context2(pStrategyC);
                        //pcon2.SetStrategy(pStrategyC);
                        V.Add(pcon2);
                        Put(pcon2.RunAlgorithm());
                    }
                }
                else
                {
                    if(S[i] == '1')
                    {
                        Context1 pcon1 = new Context1(pStrategyA);
                        V.Add(pcon1);
                        Put(pcon1.RunAlgorithm());
                    }
                    if(S[i] == '2')
                    {
                        Context2 pcon2 = new Context2(pStrategyB);
                        V.Add(pcon2);
                        Put(pcon2.RunAlgorithm());
                    }
                }
            }
        }
    }
}
