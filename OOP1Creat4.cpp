#include "pt4.h"
using namespace std;

class AbstractProductA
{
public:
    virtual void A() = 0;
    virtual string GetInfo() = 0;
    virtual string GetInfo() const = 0;
    virtual ~AbstractProductA() = default;
};

// Implement the ProductA1 and ProductA2 descendant classes
class ProductA1 : public AbstractProductA
{
public:
    ProductA1(int i) : info(to_string(i)) { }
    void A() override { int n = stoi(info); info = to_string(n * 2); }
    string GetInfo() override { return info; }
    string GetInfo() const override { return info; }
private:
    string info;
};

class ProductA2 : public AbstractProductA
{
public:
    ProductA2(int i) : info(to_string(i)) { }
    void A() override { info += info; }
    string GetInfo() override { return info; }
    string GetInfo() const override { return info; }
private:
    string info;
};

class AbstractProductB
{
public:
    virtual void B(const AbstractProductA& objA) = 0;
     virtual string GetInfo() = 0;
    virtual string GetInfo()  const = 0;
    virtual ~AbstractProductB() = default;
};

// Implement the ProductB1 and ProductB2 descendant classes
class ProductB1 : public AbstractProductB
{
public:
    ProductB1(int i) : info(to_string(i)) { }
    void B(const AbstractProductA& objA) override { info = to_string(stoi(info) + stoi(objA.GetInfo())); }
    string GetInfo() override { return info; }
    string GetInfo() const override { return info; }
private:
    string info;
};

class ProductB2 : public AbstractProductB
{
public:
    ProductB2(int i) : info(to_string(i)) { }
    void B(const AbstractProductA& objA) override { info += objA.GetInfo(); }
    string GetInfo() override { return info; }
    string GetInfo() const override { return info; }
private:
    string info;
};

class AbstractFactory
{
public:
    virtual AbstractProductA* CreateProductA(int info) = 0;
    virtual AbstractProductB* CreateProductB(int info) = 0;
    virtual ~AbstractFactory() = default;
};

// Implement the ConcreteFactory1
//   and ConcreteFactory2 descendant classes
class Factory1 : public AbstractFactory
{
public:
    AbstractProductA* CreateProductA(int info) override { return new ProductA1(info); }
    AbstractProductB* CreateProductB(int info) override { return new ProductB1(info); }
};

class Factory2 : public AbstractFactory
{
public:
    AbstractProductA* CreateProductA(int info) override { return new ProductA2(info); }
    AbstractProductB* CreateProductB(int info) override { return new ProductB2(info); }
};

AbstractFactory* Factory(int nf)
{
    if (nf == 1)
        return new Factory1();
    else
        return new Factory2();
}

void Solve()
{
    Task("OOP1Creat4");
    int nf, na, nb;
    string s;
    pt >> nf >> na >> nb >> s;
    AbstractFactory* f = Factory(nf);
    AbstractProductA* pa = f->CreateProductA(na);
    AbstractProductB* pb = f->CreateProductB(nb);
    
    for (auto i : s){
    	if (i == 'A'){
    		pa->A();
		}
        else{
        	pb->B(*pa);
		}
	}
	pt << pa->GetInfo() << pb->GetInfo();

}
