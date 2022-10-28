#include "pt4.h"
using namespace std;

class Component
{
public:
    virtual string Show() = 0;
};

// Implement the ConcreteComponent descendant class
class ConcreteComponent : public Component
{
public:
    ConcreteComponent(string s) : text(s) { }
    string Show() override { return text; }
private:
    string text;
};

class Decorator : public Component
{
protected:
    Component* comp;
};

// Implement the ConcreteDecoratorA

class ConcreteDecoratorA : public Decorator
{
public:
    ConcreteDecoratorA(Component* comp) { this->comp = comp; }
    string Show() override { return string("==") + comp->Show() + "=="; }
};

//   and ConcreteDecoratorB descendant classes
class ConcreteDecoratorB : public Decorator
{
public:
    ConcreteDecoratorB(Component* comp) { this->comp = comp; }
    string Show() override { return string("(") + comp->Show() + ")"; }
};

void Solve()
{
    Task("OOP2Struc7");
    int n;
    pt >> n;
    vector<Component*> component(n);
    for (int i = 0; i < n; i++) {
        string s1, s2;
        pt >> s1 >> s2;
        for (auto c : s2) {
            ConcreteComponent obj(s1);
            if (c == 'A') {
                ConcreteDecoratorA conA(&obj);
                s1 = conA.Show();
            }
            else {
                ConcreteDecoratorB conB(&obj);
                s1 = conB.Show();
            }
        }
        component[i] = new ConcreteComponent(s1);
    }
    for (auto it = component.rbegin(); it != component.rend(); it++)
        pt << (*it)->Show();
}
