#include "pt4.h"
using namespace std;

class Request
{
public:
    virtual string ToStr() = 0;
};

// Implement the RequestA and RequestB descendant classes
class RequestA : public Request
{
public:
    RequestA(int param) : param(param) {}
    int GetParam() { return param; }
    string ToStr() override { return string("A:") + to_string(param); }

private:
    int param;
};

class RequestB : public Request
{
public:
    RequestB(string param) : param(param) {}
    string GetParam() { return param; }
    string ToStr() override { return string("B:") + param; }

private:
    string param;
};

class Handler
{
    Handler* successor;
public:
    Handler(Handler* successor) : successor(successor) {}
    virtual void HandleRequest(Request* req);
    ~Handler();
};

void Handler::HandleRequest(Request* req)
{
    // Implement the method
    if (successor)
        successor->HandleRequest(req);
    else
        pt << string("Request ") + req->ToStr() + " not processed";
}
Handler::~Handler()
{
    // Implement the destructor
    if (successor)
        successor->~Handler();
}

// Implement the HandlerA and HandlerB descendant classes
class HandlerA : public Handler
{
public:
    HandlerA(Handler *successor, int id, int param1, int param2) : Handler(successor), id(id), param1(param1), param2(param2) {}
    void HandleRequest(Request *req) override;

private:
    int id;
    int param1, param2;
};

void HandlerA::HandleRequest(Request *req)
{
    string flag = req->ToStr();
    if (flag[0] == 'A')
    {
        int param = stoi(flag.substr(2));
        if (param >= param1 && param <= param2)
            pt << string("Request " + flag + " processed by handler " + to_string(id));
        else
            Handler::HandleRequest(req);
    }
    else
        Handler::HandleRequest(req);
}

class HandlerB : public Handler
{
public:
    HandlerB(Handler *successor, int id, string param1, string param2) : Handler(successor), id(id), param1(param1), param2(param2) {}
    void HandleRequest(Request *req) override;

private:
    int id;
    string param1, param2;
};

void HandlerB::HandleRequest(Request *req)
{
    string flag = req->ToStr();
    if (flag[0] == 'B')
    {
        string param = flag.substr(2);
        if (param >= param1 && param <= param2)
            pt << string("Request " + flag + " processed by handler " + to_string(id));
        else
            Handler::HandleRequest(req);
    }
    else
        Handler::HandleRequest(req);
}

class Client
{
    Handler* h;
public:
    Client(Handler* h) : h(h) {}
    void SendRequest(Request* req);
    ~Client();
};

void Client::SendRequest(Request* req)
{
    h->HandleRequest(req);
}
Client::~Client()
{
    // Implement the destructor
    if (h){
    	h->~Handler();
	}
}

void Solve()
{
    Task("OOP3Behav14");
    int n;
    pt >> n;
    Handler *h = new Handler(nullptr);
    for (int i = 0; i < n; i++)
    {
        char flag;
        pt >> flag;
        if (flag == 'A')
        {
            int num1, num2;
            pt >> num1 >> num2;
            h = new HandlerA(h, i, num1, num2);
        }
        else
        {
            string s1, s2;
            pt >> s1 >> s2;
            h = new HandlerB(h, i, s1, s2);
        }
    }
    Client cl(h);
    int k;
    pt >> k;
    for (int i = 0; i < k; i++)
    {
        char c;
        pt >> c;
        if (c == 'A')
        {
            int num;
            pt >> num;
            cl.SendRequest(new RequestA(num));
        }
        else
        {
            string s;
            pt >> s;
            cl.SendRequest(new RequestB(s));
        }
    }
}
