// File: "OOP1Creat6"
using PT4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PT4Tasks
{
    public class MyTask : PT
    {
        public abstract class BaseClass
        {
            int data;
            public void IncData(int increment)
            {
                data += increment;
            }
            public int GetData()
            {
                return data;
            }
        }

        public class Singleton : BaseClass
        {
            static Singleton uniqueInstance;
            Singleton() { }
            public static void Reset()
            {
                uniqueInstance = null;
            }
            // Complete the implementation of the class
            public static Singleton Instance()
            {
                if (uniqueInstance == null)
                    uniqueInstance = new Singleton();
                return uniqueInstance;
            }
            public static int InstanceCount()
            {
                if (uniqueInstance == null)
                    return 0;
                else
                    return 1;
            }
        }
        static Singleton[] uniqueInstance = null;
        public class Doubleton : BaseClass
        {
            static Doubleton[] instances = new Doubleton[2];
            Doubleton() { }
            public static void Reset()
            {
                instances[0] = instances[1] = null;
            }
            // Complete the implementation of the class
            public static Doubleton Instance1()
            {
                if (instances[0] == null)
                    instances[0] = new Doubleton();
                return instances[0];
            }
            public static Doubleton Instance2()
            {
                if (instances[1] == null)
                    instances[1] = new Doubleton();
                return instances[1];
            }
            public static int InstanceCount()
            {
                if (instances[0] == null && instances[1] == null)
                    return 0;
                else if (instances[0] != null && instances[1] != null)
                    return 2;
                else
                    return 1;
            }
        }
        public class Tenton : BaseClass
        {
            static Tenton[] instances = new Tenton[10];
            Tenton() { }
            public static void Reset()
            {
                for (int i = 0; i < instances.Length; i++)
                    instances[i] = null;
            }
            // Complete the implementation of the class
            public static Tenton Instance(int a)
            {
                if (instances[a] == null)
                    instances[a] = new Tenton();
                return instances[a];
            }
            public static int InstanceCount()
            {
                int i = 0;
                for (int p = 0; p < 10; p++)
                {
                    if (instances[p] != null)
                        i++;
                }
                return i;
            }
        }
        static Tenton[] instance = new Tenton[10];
        public static void Solve()
        {
            Task("OOP1Creat6");
            int n = GetInt(), k;
            BaseClass[] baseClasses = new BaseClass[n];
            Singleton.Reset();
            Doubleton.Reset();
            Tenton.Reset();
            for (int i = 0; i < n; i++)
            {
                string s = GetString();
                switch (s[0])
                {
                    case 'S':
                        baseClasses[i] = Singleton.Instance();
                        break;
                    case 'D':
                        if (s[1] == '1')
                            baseClasses[i] = Doubleton.Instance1();
                        else
                            baseClasses[i] = Doubleton.Instance2();
                        break;
                    case 'T':
                        baseClasses[i] = Tenton.Instance(s[1] - '0');
                        break;
                }
            }
            Put(Singleton.InstanceCount(), Doubleton.InstanceCount(), Tenton.InstanceCount());
            k = GetInt();
            for (int i = 0; i < k; i++)
            {
                int p, q;
                (p, q) = GetInt2();
                baseClasses[p].IncData(q);
            }
            for (int i = 0; i < n; i++)
            {
                Put(baseClasses[i].GetData());
            }
        }
    }
}
