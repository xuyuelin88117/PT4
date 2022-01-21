#include "pt4.h"
using namespace std;

class BaseClass
{
    int data = 0;

public:
    void IncData(int increment);
    int GetData();
};

void BaseClass::IncData(int increment)
{
    data += increment;
}
int BaseClass::GetData()
{
    return data;
}

class Singleton : public BaseClass
{
    static Singleton *uniqueInstance;
    Singleton() {}
    ~Singleton() {}

public:
    // Complete the implementation of the class
    static Singleton *Instance()
    {
        if (uniqueInstance == nullptr)
            uniqueInstance = new Singleton;
        return uniqueInstance;
    }
    static int InstanceCount()
    {
        if (uniqueInstance == nullptr)
            return 0;
        else
            return 1;
    }
};
Singleton *Singleton::uniqueInstance = nullptr;

class Doubleton : public BaseClass
{
    static Doubleton *instances[];
    Doubleton() {}
    ~Doubleton() {}

public:
    // Complete the implementation of the class
    static Doubleton *Instance1()
    {
        if (instances[0] == nullptr)
            instances[0] = new Doubleton;
        return instances[0];
    }
    static Doubleton *Instance2()
    {
        if (instances[1] == nullptr)
            instances[1] = new Doubleton;
        return instances[1];
    }
    static int InstanceCount()
    {
        if (instances[0] == nullptr && instances[1] == nullptr)
            return 0;
        else if (instances[0] != nullptr && instances[1] != nullptr)
            return 2;
        else
            return 1;
    }
};

Doubleton *Doubleton::instances[2];

class Tenton : public BaseClass
{
    static Tenton *instances[];
    Tenton() {}
    ~Tenton() {}

public:
    // Complete the implementation of the class
    static Tenton *Instance(int a)
    {
        if (instances[a] == nullptr)
            instances[a] = new Tenton;
        return instances[a];
    }
    static int InstanceCount()
    {
        int i = 0;
        for (int p = 0; p < 10; p++)
        {
            if (instances[p] != nullptr)
                i++;
        }
        return i;
    }
};
Tenton *Tenton::instances[10];

void Solve()
{
    Task("OOP1Creat6");
    int n, k;
    pt >> n;
    BaseClass **base = new BaseClass *[n];
    for (int i = 0; i < n; i++)
    {
        string s;
        pt >> s;
        switch (s[0])
        {
        case 'S':
            base[i] = Singleton::Instance();
            break;
        case 'D':
            if (s[1] == '1')
                base[i] = Doubleton::Instance1();
            else
                base[i] = Doubleton::Instance2();
            break;
        case 'T':
            base[i] = Tenton::Instance(s[1] - '0');
            break;
        }
    }
    pt << Singleton::InstanceCount() << Doubleton::InstanceCount() << Tenton::InstanceCount();
    pt >> k;
    for (int i = 0; i < k; i++)
    {
        int p, q;
        pt >> p >> q;
        base[p]->IncData(q);
    }
    for (int i = 0; i < n; i++)
    {
        pt << base[i]->GetData();
    }
}
