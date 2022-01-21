#include "pt4.h"
using namespace std;

class Validator
{
public:
    virtual string Validate(string s);
};

string Validator::Validate(string s)
{
    return "";
}

// Implement the EmptyValidator, NumberValidator
//   and RangeValidator descendant classes
class EmptyValidator : public Validator
{
public:
    string Validate(string s) override
    {
        if (s.empty())
            return "!Empty text";
        else
            return "";
    }
};

class NumberValidator : public Validator
{
public:
    string Validate(string s) override;
};

string NumberValidator::Validate(string s)
{
    if (s.empty())
        return string("!'") + s + "': not a number";
    for (int i = 0; i < s.length(); i++)
        if (!isdigit(s[i]) && (i || s[i] != '-'))
            return string("!'") + s + "': not a number";
    return "";
}

class RangeValidator : public Validator
{
public:
    RangeValidator(int a, int b)
    {
        minInt = min(a, b);
        maxInt = max(a, b);
    }
    string Validate(string s) override;

private:
    int minInt, maxInt;
};

string RangeValidator::Validate(string s)
{
    if (s.empty())
        return string("!'") + s + "': not in range " + to_string(minInt) + ".." + to_string(maxInt);
    for (int i = 0; i < s.length(); i++)
        if (!isdigit(s[i]) && (i || s[i] != '-'))
            return string("!'") + s + "': not in range " + to_string(minInt) + ".." + to_string(maxInt);
    int n = stoi(s);
    if (n < minInt || n > maxInt)
        return string("!'") + s + "': not in range " + to_string(minInt) + ".." + to_string(maxInt);
    return "";
}

// Implement the TextBox and TextForm classes
class TextBox
{
public:
    TextBox() : text("") { v = new Validator(); }
    ~TextBox() { delete v; }
    void setText(string text) { this->text = text; }
    void setValidator(Validator *v)
    {
        delete this->v;
        this->v = v;
    }
    string Validate() { return v->Validate(text); }
    string Validate() const { return v->Validate(text); }

private:
    string text;
    Validator *v;
};

class TextForm
{
public:
    TextForm(int n) { tb.resize(n); }
    void SetText(int ind, string text) { tb[ind].setText(text); }
    void SetValidator(int ind, Validator *v) { tb[ind].setValidator(v); }
    string Validate()
    {
        string s;
        for (const auto &t : tb)
            s += t.Validate();
        return s;
    }

private:
    vector<TextBox> tb;
};
void Solve()
{
    Task("OOP3Behav4");
    int n, a, b, k;
    pt >> n >> a >> b >> k;
    vector<pair<int, char>> v;
    for (int i = 0; i < k; i++)
    {
        int in;
        char c;
        pt >> in >> c;
        v.push_back({in, c});
    }
    for (int i = 0; i < 5; i++)
    {
        TextForm t(n);
        string s;
        for (int i = 0; i < n; i++)
        {
            pt >> s;
            t.SetText(i, s);
        }
        for (auto &p : v)
        {
            int in = p.first;
            char c = p.second;
            if (c == 'E')
                t.SetValidator(in, new EmptyValidator());
            else if (c == 'N')
                t.SetValidator(in, new NumberValidator());
            else
                t.SetValidator(in, new RangeValidator(a, b));
        }
        pt << t.Validate();
    }

}
