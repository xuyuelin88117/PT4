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
                if (uniqueInstance == null) uniqueInstance = new Singleton();
                return uniqueInstance;
            }
            public static int InstanceCount()
            {
                int x;
                if (uniqueInstance == null) x = 0;
                else x = 1;
                return x;
            }

        }

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
                if (instances[0] == null) instances[0] = new Doubleton();
                return instances[0];
            }
            public static Doubleton Instance2()
            {
                if (instances[1] == null) instances[1] = new Doubleton();
                return instances[1];
            }
            public static int InstanceCount()
            {
                int cnt = 0;
                for (int i = 0; i < 2; i++)
                {
                    if (instances[i] != null) cnt++;
                }
                return cnt;
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
            public static Tenton Instance(int index)
            {
                if (instances[index] == null) instances[index] = new Tenton();
                return instances[index];
            }
            public static int InstanceCount()
            {
                int cnt = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (instances[i] != null) cnt++;
                }
                return cnt;
            }
        }

        public static void Solve()
        {
            Task("OOP1Creat6");
            int n = GetInt();
            BaseClass[] b = new BaseClass[n];
            Singleton.Reset();
            Doubleton.Reset();
            Tenton.Reset();
            for (int i = 0; i < n; i++)
            {
                string s = GetString();
                if (s[0] == 'S') b[i] = Singleton.Instance();
                if (s[0] == 'D')
                {
                    if (s[1] == '1') b[i] = Doubleton.Instance1();
                    else b[i] = Doubleton.Instance2();
                }
                if (s[0] == 'T')
                {
                    int index = s[1] - '0';
                    b[i] = Tenton.Instance(index);
                }
            }
            Put(Singleton.InstanceCount());
            Put(Doubleton.InstanceCount());
            Put(Tenton.InstanceCount());
            int k = GetInt();
            for (int i = 1; i <= k; i++)
            {
                int index = GetInt(), x = GetInt();
                b[index].IncData(x);
            }
            for (int i = 0; i < n; i++)
            {
                Put(b[i].GetData());
            }
        }
    }
}
