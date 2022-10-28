#include "pt4.h"
#include <algorithm>
using namespace std;

class Implementor
{
public:
    virtual string DrawLine(int size) = 0;
    virtual string DrawText(string text) = 0;
};

// Implement the ConcreteImplementorA
//   and ConcreteImplementorB descendant classes
class ConcreteImplementorA : public Implementor
{
public:
    int num;
    string s;
    ConcreteImplementorA(int size, string text)
    {
        num = size;
        s = text;
    }
    string DrawLine(int size) override
    {
        string s1;
        for (int i = 0; i < num; i++)
        {
            s1 = s1 + '-';
        }
        return s1;
    }
    string DrawText(string text) override
    {
        transform(s.begin(), s.end(), s.begin(), ::tolower);
        return s;
    }
};

class ConcreteImplementorB : public Implementor
{
public:
    int num;
    string s;
    ConcreteImplementorB(int size, string text)
    {
        num = size;
        s = text;
    }
    string DrawLine(int size) override
    {
        string s1;
        for (int i = 0; i < num; i++)
        {
            s1 = s1 + '=';
        }
        return s1;
    }
    string DrawText(string text) override
    {
        transform(s.begin(), s.end(), s.begin(), ::toupper);
        return s;
    }
};

class Abstraction
{
protected:
    int size;
    Implementor *imp;

public:
    Abstraction(Implementor *imp, int size) : imp(imp), size(size){};
    // Complete the implementation of the class Abstraction
    virtual string ShowA()
    {
        return imp->DrawLine(size);
    }
    virtual string ShowB()
    {
        return imp->DrawLine(size);
    }
    virtual void setsize(int n)
    {
        size = n;
    }
};

// Implement the RefinedAbstraction descendant class
// RefinedAbstraction descendant class
class RefinedAbstraction : public Abstraction
{
public:
    string text;
    RefinedAbstraction(Implementor *imp, int size, string text) : Abstraction(imp, size), text(text){};
    string ShowA() override
    {
        string s;
        s = '-' + imp->DrawText(text);
        if (s.size() >= size)
        {
            s = s.substr(0, size);
        }
        else
        {
            int k = size - s.size();
            for (int i = 0; i < k; i++)
            {
                s = s + '-';
                ShowLine(s);
                ShowLine(size - s.size());
            }
        }

        return s;
    }
    string ShowB() override
    {
        string s;
        s = '=' + imp->DrawText(text);
        if (s.size() >= size)
        {
            s = s.substr(0, size);
        }
        else
        {
            int k = size - s.size();
            for (int i = 0; i < k; i++)
            {
                s = s + '=';
            }
        }
        transform(s.begin(), s.end(), s.begin(), ::toupper);
        return s;
    }
};

void Solve()
{
    Task("OOP2Struc10");
    int size;
    string s;
    pt >> size >> s;
    Implementor *IMP_1 = new ConcreteImplementorA(size, s);
    Implementor *IMP_2 = new ConcreteImplementorB(size, s);
    Abstraction *ABS = new RefinedAbstraction(IMP_1, size, s);
    ABS->setsize(size);

    pt << IMP_1->DrawLine(size) << IMP_2->DrawLine(size) << ABS->ShowA() << ABS->ShowB();

    for (int i = 0; i < 5; i++)
    {
        int num;
        pt >> num;
        Implementor *Test_IMP_1 = new ConcreteImplementorA(num, s);
        Implementor *TEST_IMP_2 = new ConcreteImplementorB(num, s);
        Abstraction *TEST_ABS = new RefinedAbstraction(Test_IMP_1, size, s);
        TEST_ABS->setsize(num);
        pt << Test_IMP_1->DrawLine(num) << TEST_IMP_2->DrawLine(num) << TEST_ABS->ShowA() << TEST_ABS->ShowB();
    }
}
