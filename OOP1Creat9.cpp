#include "pt4.h"
using namespace std;

class Builder
{
public:
    virtual void BuildStart() {}
    virtual void BuildPartA() {}
    virtual void BuildPartB() {}
    virtual void BuildPartC() {}
    virtual string GetResult() = 0;
};

// Implement the ConcreteBuilder1
//   and ConcreteBuilder2 descendant classes
class ConcreteBuilder1 : public Builder
{
    string info;

public:
    ConcreteBuilder1() {}
    void BuildStart()
    {
        this->info = "";
    }
    void BuildPartA()
    {
        info = info + "-1-";
    }
    void BuildPartB()
    {
        info = info + "-2-";
    }
    void BuildPartC()
    {
        info = info + "-3-";
    }
    string GetResult()
    {
        return info;
    }
};

class ConcreteBuilder2 : public Builder
{
    string info;

public:
    ConcreteBuilder2() {}
    void BuildStart()
    {
        this->info = "";
    }
    void BuildPartA()
    {
        info = info + "=*=";
    }
    void BuildPartB()
    {
        info = info + "=**=";
    }
    void BuildPartC()
    {
        info = info + "=***=";
    }
    string GetResult()
    {
        return info;
    }
};

class Director
{
    Builder* b;
public:
    Director(Builder* b);
    string GetResult();
    void Construct(string templat);
};

Director::Director(Builder* b)
{
    this->b = b;
}
string Director::GetResult()
{
    return b->GetResult();
}
void Director::Construct(string templat)
{
    // Implement the method
    b->BuildStart();
    for (int i = 0; templat[i]; i++)
    {
        if (templat[i] == 'A')
            b->BuildPartA();
        else
        {
            if (templat[i] == 'B')
                b->BuildPartB();
            else
            {
                if (templat[i] == 'C')
                    b->BuildPartC();
            }
        }
    }
}

void Solve()
{
    Task("OOP1Creat9");
    Builder *Builder1 = new ConcreteBuilder1();
    Builder *Builder2 = new ConcreteBuilder2();
    Director *Director1 = new Director(Builder1);
    Director *Director2 = new Director(Builder2);
    
    for (int i = 0; i < 5; i++)
    {
        string s;
        pt >> s;
        Director1->Construct(s);
        pt << Director1->GetResult();
        Director2->Construct(s);
        pt << Director2->GetResult();
    }
    
    delete Builder1;
    Builder1 = NULL;
    delete Builder2;
    Builder2 = NULL;
    delete Director1;
    Director1 = NULL;
    delete Director2;
    Director2 = NULL;
}
