#include "pt4.h"
using namespace std;

class Prototype
{
public:
    virtual Prototype *Clone() = 0;
    virtual void ChangeId(int id) = 0;
    virtual string GetInfo() = 0;
};

// Implement the ConcretePrototype1
//   and ConcretePrototype2 descendant classes
class ConcretePrototype1 : public Prototype
{
private:
    string data;
    int id;

public:
    ConcretePrototype1(string s, int n)
    {
        data = s;
        id = n;
    }
    Prototype *Clone();
    void ChangeId(int id);
    string GetInfo() { return "CP1=" + data + "=" + to_string(id); };
};
Prototype *ConcretePrototype1::Clone()
{
    Prototype *prot = new ConcretePrototype1(data, id);
    return prot;
}
void ConcretePrototype1::ChangeId(int id)
{
    this->id = id;
}

class ConcretePrototype2 : public Prototype
{
private:
    string data;
    int id;

public:
    ConcretePrototype2(string s, int n)
    {
        data = s;
        id = n;
    }
    Prototype *Clone();
    void ChangeId(int id);
    string GetInfo() { return "CP2=" + data + "=" + to_string(id); };
};
Prototype *ConcretePrototype2::Clone()
{
    Prototype *prot = new ConcretePrototype2(data, id);
    return prot;
}
void ConcretePrototype2::ChangeId(int id)
{
    this->id = id;
}

class Client
{
private:
    Prototype *p;
    vector<Prototype *> V;
    // Add required fields
public:
    Client(Prototype *p);
    ~Client();
    void AddObject(int id);
    string GetObjects();
};

Client::Client(Prototype *p)
{
    // Implement the constructor
    this->p = p;
}
Client::~Client()
{
    // Implement the destructor
    for (auto e : V)
        delete e;
}
void Client::AddObject(int id)
{
    // Implement the method
    Prototype *prot = this->p->Clone();
    prot->ChangeId(id);
    V.push_back(prot);
}
string Client::GetObjects()
{
    string s;
    int i = 0;
    for (auto e : V)
    {
        s += e->GetInfo();
        if (i == V.size() - 1)
            break;
        s += " ";
        i++;
    }
    return s;
    // Remove the previous statement and implement the method
}

void Solve()
{
    Task("OOP1Creat7");
    int n;
    string s;
    pt >> s >> n;
    ConcretePrototype1 *p1 = new ConcretePrototype1(s, 0);
    ConcretePrototype2 *p2 = new ConcretePrototype2(s, 0);
    Client *Clt1 = new Client(p1);
    Client *Clt2 = new Client(p2);

    for (int i = 0; i < n; i++)
    {
        int id;
        pt >> id;
        Clt1->AddObject(id);
        Clt2->AddObject(id);
    }
    pt << Clt1->GetObjects();
    pt << Clt2->GetObjects();

    delete Clt1;
    delete Clt2;
}
