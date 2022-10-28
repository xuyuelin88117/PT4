#include "pt4.h"
#include <numeric>
using namespace std;

class Iterator
{
public:
    virtual void First() = 0;
    virtual void Next() = 0;
    virtual bool IsDone() = 0;
    virtual int CurrentItem() = 0;
};

class Aggregate
{
public:
    virtual Iterator* CreateIterator() = 0;
};

// Implement the ConcreteAggregateA, ConcreteAggregateB
//   and ConcreteAggregateC descendant classes

class ConcreteAggregateA : public Aggregate
{
public:
    ConcreteAggregateA(int data) : data(data) {}
    Iterator *CreateIterator() override;
    int data;
    int divied = 1;
};

class ConcreteAggregateB : public Aggregate
{
public:
    ConcreteAggregateB(string data) : data(data) {}
    Iterator *CreateIterator() override;
    string data;
    int index = 0;
};

class ConcreteAggregateC : public Aggregate
{
public:
    ConcreteAggregateC(vector<int> data) : data(data) {}
    Iterator *CreateIterator() override;
    vector<int> data;
    int index = 0;
    int divied = 1;
};
// Implement the ConcreteIteratorA, ConcreteIteratorB
//   and ConcreteIteratorC descendant classes
class ConcreteIteratorA : public Iterator
{
public:
    ConcreteIteratorA(ConcreteAggregateA *aggr) : aggr(aggr) {}
    virtual void First() override { aggr->divied = 1; }
    virtual void Next() override { aggr->divied *= 10; }
    virtual bool IsDone() override { return aggr->data && aggr->data / aggr->divied == 0 || !aggr->data && aggr->divied == 10; }
    virtual int CurrentItem() override { return abs(aggr->data / aggr->divied % 10); }

private:
    ConcreteAggregateA *aggr;
};

class ConcreteIteratorB : public Iterator
{
public:
    ConcreteIteratorB(ConcreteAggregateB *aggr) : aggr(aggr) {}
    virtual void First() override
    {
        aggr->index = aggr->data.length() - 1;
        while (aggr->index >= 0 && !isdigit(aggr->data[aggr->index]))
            aggr->index--;
    }
    virtual void Next() override
    {
        aggr->index--;
        while (aggr->index >= 0 && !isdigit(aggr->data[aggr->index]))
            aggr->index--;
    }
    virtual bool IsDone() override { return aggr->index < 0; }
    virtual int CurrentItem() override { return aggr->data[aggr->index] - '0'; }

private:
    ConcreteAggregateB *aggr;
};

class ConcreteIteratorC : public Iterator
{
public:
    ConcreteIteratorC(ConcreteAggregateC *aggr) : aggr(aggr) {}
    virtual void First() override
    {
        aggr->index = aggr->data.size() - 1;
        aggr->divied = 1;
    }
    virtual void Next() override;
    virtual bool IsDone() override { return aggr->index < 0; }
    virtual int CurrentItem() override { return abs(aggr->data[aggr->index] / aggr->divied % 10); }

private:
    ConcreteAggregateC *aggr;
};

void ConcreteIteratorC::Next()
{
    aggr->divied *= 10;
    int data = aggr->data[aggr->index];
    int divied = aggr->divied;
    if (data && data / divied == 0 || !data && divied == 10)
    {
        aggr->index--;
        aggr->divied = 1;
    }
}

Iterator *ConcreteAggregateA::CreateIterator()
{
    return new ConcreteIteratorA(this);
}
Iterator *ConcreteAggregateB::CreateIterator()
{
    return new ConcreteIteratorB(this);
}
Iterator *ConcreteAggregateC::CreateIterator()
{
    return new ConcreteIteratorC(this);
}

void Solve()
{
    Task("OOP3Behav7");
    int n;
    pt >> n;
    vector<Aggregate *> a(n);
    for (int i = 0; i < n; i++)
    {
        char c;
        pt >> c;
        if (c == 'A')
        {
            int num;
            pt >> num;
            a[i] = new ConcreteAggregateA(num);
        }
        else if (c == 'B')
        {
            string num;
            pt >> num;
            a[i] = new ConcreteAggregateB(num);
        }
        else
        {
            int k, num;
            pt >> k;
            vector<int> v(k);
            for (int i = 0; i < k; i++)
            {
                pt >> num;
                v[i] = num;
            }
            a[i] = new ConcreteAggregateC(v);
        }
    }

    for (auto it = a.rbegin(); it != a.rend(); it++)
    {
        vector<int> v2;
        auto it2 = (*it)->CreateIterator();
        for (it2->First(); !it2->IsDone(); it2->Next())
            v2.push_back(it2->CurrentItem());
        pt << accumulate(v2.begin(), v2.end(), 0);
        for (auto i : v2)
            pt << i;
    }
}
