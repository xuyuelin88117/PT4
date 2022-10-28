// File: "OOP3Behav2"
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
            public List<Observer> removeObservers = new List<Observer>();
            public abstract void Attach(Observer observ);
            public abstract void Detach(Observer observ);
            public abstract void SetInfo(string info);
        }

        // Implement the ConcreteSubject descendant class

        public class ConcreteSubject : Subject
        {
            public List<Observer> obs = new List<Observer>();
            
            public override void Attach(Observer o)
            {
                obs.Add(o);
            }

            public override void Detach(Observer o)
            {
                obs.Remove(o);
            }

            public override void SetInfo(string i)
            {
                foreach (Observer ob in obs)
                {
                    ob.OnInfo(this, i);
                }
                foreach (Observer ob in removeObservers)
                {
                    obs.Remove(ob);
                }
            }
        }

        public abstract class Observer
        {
            public abstract void OnInfo(Subject sender, string info);
        }

        // Implement the ConcreteObserver descendant class

        public class ConcreteObserver : Observer
        {
            private string l;
            private char c;

            public ConcreteObserver(char c) 
            {
                this.c = c;
                l = "";
            }

            public override void OnInfo(Subject s, string str)
            {
                l += str;
                if (c == str[str.Length - 1]) {
                    //Detach(sender);
                    s.removeObservers.Add(this);
                }
            }
            public void Attach(Subject sub)
            {
                sub.Attach(this);
            }
            public void Detach(Subject sub)
            {
                sub.Detach(this);
            }
            public string GetLog()
            {
                return l;
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav2");

            int n = GetInt();
            int m = GetInt();

            string[] ms = new string[m];
            for (int i = 0; i < m; i++) {
                ms[i] = GetString();
            }

            ConcreteSubject s1 = new ConcreteSubject();
            ConcreteSubject s2 = new ConcreteSubject();

            ConcreteObserver[] obs = new ConcreteObserver[n];
            for (int i = 0; i < n; i++) {
                obs[i] = new ConcreteObserver(Convert.ToChar(97 + i));
                obs[i].Attach(s1);
                obs[i].Attach(s2);
            }

            for (int i = 0; i < m; i++)
            {
                if (ms[i][0] == '1')
                    s1.SetInfo(ms[i]);
                else
                    s2.SetInfo(ms[i]);
            }

            for (int i = 0; i < n; i++)
            {
                Put(obs[i].GetLog());
            }
        }
    }
}
