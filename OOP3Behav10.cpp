#include "pt4.h"
using namespace std;

class State
{
public:
    virtual string GetNextToken() = 0;
};

// Implement the Context class
class Context
{
public:
    Context(string &text);
    char GetCharAt(int index) { return text[index]; }
    void SetState(State *newState)
    {
        if (prevState)
            delete prevState;
        prevState = currentState;
        currentState = newState;
    }
    string GetNestToken() { return currentState->GetNextToken(); }

private:
    string text;
    State *currentState;
    State *prevState;
};
// Implement the ConcreteStateNormal, ConcreteStateString,
//   ConcreteStateComm and ConcreteStateFin descendant classes
class ConcreteSatateNormal : public State
{
public:
    ConcreteSatateNormal(Context *ct, int index) : ct(ct), index(index) {}
    string GetNextToken() override;

private:
    Context *ct;
    int index;
};

class ConcreteSatateString : public State
{
public:
    ConcreteSatateString(Context *ct, int index) : ct(ct), index(index) {}
    string GetNextToken() override;

private:
    Context *ct;
    int index;
};

class ConcreteSatateComm : public State
{
public:
    ConcreteSatateComm(Context *ct, int index) : ct(ct), index(index) {}
    string GetNextToken() override;

private:
    Context *ct;
    int index;
};

class ConcreteSatateFinal : public State
{
public:
    string GetNextToken() override { return ""; }
};

Context::Context(string &text) : text(text), currentState(new ConcreteSatateNormal(this, 0)), prevState(nullptr) {}

string ConcreteSatateNormal::GetNextToken()
{
    string s = "Normal:";
    while (true)
    {
        char c = ct->GetCharAt(index++);
        if (c == '"')
        {
            ct->SetState(new ConcreteSatateString(ct, index));
            break;
        }
        else if (c == '{')
        {
            ct->SetState(new ConcreteSatateComm(ct, index));
            break;
        }
        else if (c == '.')
        {
            ct->SetState(new ConcreteSatateFinal());
            break;
        }
        else
            s += c;
    }
    return s;
}

string ConcreteSatateString::GetNextToken()
{
    string s, title = "String:";
    while (true)
    {
        char c1 = ct->GetCharAt(index++);
        char c2 = ct->GetCharAt(index);
        if (c1 == '"' && c2 == '"')
        {
            s += c1;
            index++;
        }
        else if (c1 == '"')
        {
            ct->SetState(new ConcreteSatateNormal(ct, index));
            break;
        }
        else if (c1 == '.')
        {
            title = "ErrString:";
            ct->SetState(new ConcreteSatateFinal());
            break;
        }
        else
            s += c1;
    }
    return title + s;
}

string ConcreteSatateComm::GetNextToken()
{
    string s, title = "Comm:";
    while (true)
    {
        char c = ct->GetCharAt(index++);
        if (c == '}')
        {
            ct->SetState(new ConcreteSatateNormal(ct, index));
            break;
        }
        else if (c == '.')
        {
            title = "ErrComm:";
            ct->SetState(new ConcreteSatateFinal());
            break;
        }
        else
            s += c;
    }
    return title + s;
}

void Solve()
{
    Task("OOP3Behav10");
    string s;
    pt >> s;
    Context con(s);
    while ((s = con.GetNestToken()) != "")
        pt << s;
}
