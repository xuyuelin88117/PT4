#include "pt4.h"
using namespace std;

class AbstractClass
{
public:
    virtual string PrimitiveOperation() = 0;
    // Implement the TemplateMethod, BasicOperation1,
    virtual string TemplateMethod() { return BasicOperation1() + PrimitiveOperation() + BasicOperation2() + HookOperation(); }
    //   BasicOperation2 and HookOperation methods
    virtual string BasicOperation1() { return "Boil water"; }
    virtual string BasicOperation2() { return "=Pour into a cup"; }
    protected:
    virtual string HookOperation() { return ""; }
};

// Implement the ConcreteClass1, ConcreteClass2,
class ConcreteClass1 : public AbstractClass
{
public:
    string PrimitiveOperation() override { return "=Brew tea"; }
};

class ConcreteClass2 : public AbstractClass
{
public:
    string PrimitiveOperation() override { return "=Brew coffee"; }
};

//   ConcreteClass3 and ConcreteClass4 descendant classes
class ConcreteClass3 : public ConcreteClass1
{
public:
    string HookOperation() override { return "=Add sugar and lemon"; }
};

class ConcreteClass4 : public ConcreteClass2
{
public:
    string HookOperation() override { return "=Add sugar and milk"; }
};

void Solve()
{
    Task("OOP3Behav5");
    int n;
    pt >> n;
    vector<int> v(n);
    for (int i = 0; i < n; i++)
        pt >> v[i];
	vector<AbstractClass *> ab(n);
    for (int i = 0; i < n; i++)
    {
        int t = v[i];
        if (t == 1)
            ab[i] = new ConcreteClass1();
        else if (t == 2)
            ab[i] = new ConcreteClass2();
        else if (t == 3)
            ab[i] = new ConcreteClass3();
        else
            ab[i] = new ConcreteClass4();
    }

    for (auto it = ab.rbegin(); it != ab.rend(); it++)
        pt << (*it)->TemplateMethod();

}
