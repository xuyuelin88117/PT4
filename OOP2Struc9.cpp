#include "pt4.h"
using namespace std;

class Subject
{
public:
    virtual string OperationA() = 0;
    virtual string OperationB() = 0;
    virtual string OperationC() = 0;
    virtual string OperationD() = 0;
};

// Implement the RealSubject and Proxy descendant classes
class RealSubject : public Subject
{
public:
    string OperationA() override { return "A (Real)"; }
    string OperationB() override { return "B (Real)"; }
    string OperationC() override { return "C (Real)"; }
    string OperationD() override { return "D (Real)"; }
};

class Proxy : public Subject
{
public:
    Proxy(bool deferredMode, bool protectedMode);
    ~Proxy() { if (rsubj) delete rsubj; }
    string OperationA() override;
    string OperationB() override;
    string OperationC() override;
    string OperationD() override;
private:
    bool deferredMode, protectedMode;
    RealSubject* rsubj;
};

Proxy::Proxy(bool deferredMode, bool protectedMode) : deferredMode(deferredMode), protectedMode(protectedMode)
{
    if (!deferredMode)
        rsubj = new RealSubject();
    else
        rsubj = nullptr;
}
string Proxy::OperationA()
{
    if (rsubj)
        return rsubj->OperationA();
    else
        return "A (Proxy)";
}
string Proxy::OperationB()
{
    if (protectedMode)
        return "B denied";
    else if (rsubj)
        return rsubj->OperationB();
    else
        return "B (Proxy)";
}
string Proxy::OperationC()
{
    if (!rsubj) {
        rsubj = new RealSubject();
        return rsubj->OperationC();
    }
    else
        return rsubj->OperationC();
}
string Proxy::OperationD()
{
    if (protectedMode)
        return "D denied";
    else if (!rsubj) {
        rsubj = new RealSubject();
        return rsubj->OperationD();
    }
    else
        return rsubj->OperationD();
}

string Operation(Subject& s, char c)
{
    if (c == 'A')
        return s.OperationA();
    else if (c == 'B')
        return s.OperationB();
    else if (c == 'C')
        return s.OperationC();
    else
        return s.OperationD();
}

string Operation(RealSubject& s, char c)
{
    if (c == 'A')
        return s.OperationA();
    else if (c == 'B')
        return s.OperationB();
    else if (c == 'C')
        return s.OperationC();
    else
        return s.OperationD();
}

void Solve()
{
    Task("OOP2Struc9");
    string s;
    vector<int> v(3);
    pt >> v[0] >> v[1] >> v[2] >> s;
    vector<vector<int>> V = { {0,0}, {1,0}, {0,1}, {1,1} };
    for (int i = 0; i < 3; i++) {
        if (v[i] == -1) {
            RealSubject r;
            for (auto j : s)
                pt << Operation(r, j);
        }
        else {
            bool a = V[v[i]][0], b = V[v[i]][1];
            Proxy p(a, b);
            for (auto j : s)
                pt << Operation(p, j);
        }
    }

}
