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
            public List<Observer> observers = new List<Observer>();
            
            public override void Attach(Observer observ)
            {
                observers.Add(observ);
            }

            public override void Detach(Observer observ)
            {
                observers.Remove(observ);
            }

            public override void SetInfo(string info)
            {
                foreach (Observer observ in observers)
                {
                    observ.OnInfo(this, info);
                }
                foreach (Observer observ in removeObservers)
                {
                    observers.Remove(observ);
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
            private string log;
            private char detachInfo;

            public ConcreteObserver(char detachInfo) 
            {
                this.detachInfo = detachInfo;
                log = "";
            }

            public override void OnInfo(Subject sender, string info)
            {
                log += info;
                if (detachInfo == info[info.Length - 1]) {
                    //Detach(sender);
                    sender.removeObservers.Add(this);
                }
            }
            public void Attach(Subject subj)
            {
                subj.Attach(this);
            }
            public void Detach(Subject subj)
            {
                subj.Detach(this);
            }
            public string GetLog()
            {
                return log;
            }
        }

        public static void Solve()
        {
            Task("OOP3Behav2");

            int N = GetInt();
            int K = GetInt();

            string[] masStr = new string[K];
            for (int i = 0; i < K; i++) {
                masStr[i] = GetString();
            }

            ConcreteSubject subj1 = new ConcreteSubject();
            ConcreteSubject subj2 = new ConcreteSubject();

            ConcreteObserver[] observers = new ConcreteObserver[N];
            for (int i = 0; i < N; i++) {
                observers[i] = new ConcreteObserver(Convert.ToChar(97 + i));
                observers[i].Attach(subj1);
                observers[i].Attach(subj2);
            }

            for (int i = 0; i < K; i++)
            {
                if (masStr[i][0] == '1')
                    subj1.SetInfo(masStr[i]);
                else
                    subj2.SetInfo(masStr[i]);
            }

            for (int i = 0; i < N; i++)
            {
                Put(observers[i].GetLog());
            }
        }
    }
}
