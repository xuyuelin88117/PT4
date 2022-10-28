#include "pt4.h"
using namespace std;
// Implement the Client, ReceiverA and ReceiverB classes
class Client
{
public:
    Client() : info(string()) {}
    void AddLeft(string newInfo) { info = newInfo + info; }
    void AddRight(string newInfo) { info += newInfo; }
    string GetInfo() { return info; }

private:
    string info;
};

class ReceiverA
{
public:
    ReceiverA(Client *cli, string info) : cli(cli), info(info) {}
    void ActionA() { cli->AddLeft(info); }

private:
    Client *cli;
    string info;
};

class ReceiverB
{
public:
    ReceiverB(Client *cli, string info) : cli(cli), info(info) {}
    void ActionB() { cli->AddRight(info); }

private:
    Client *cli;
    string info;
};

class Command
{
public:
    virtual void Execute() = 0;
};

// Implement the ConcreteCommandA
class ConcreteCommandA : public Command
{
public:
    ConcreteCommandA(ReceiverA *recv) : recv(recv) {}
    void Execute() override { recv->ActionA(); }

private:
    ReceiverA *recv;
};
//   and ConcreteCommandB descendant classes
class ConcreteCommandB : public Command
{
public:
    ConcreteCommandB(ReceiverB *recv) : recv(recv) {}
    void Execute() override { recv->ActionB(); }

private:
    ReceiverB *recv;
};
class Invoker
{
    Command* cmd;
public:
    Invoker(Command* cmd) : cmd(cmd) {}
    void Invoke();
};

void Invoker::Invoke()
{
    cmd->Execute();
}

void Solve()
{
    Task("OOP3Behav8");
    int n;
    pt >> n;
    Client cli;
    vector<Command *> com(n);
    vector<string> v(n);
    for (int i = 0; i < n; i++)
    {
        pt >> v[i];
        if (v[i][0] == 'A')
            com[i] = new ConcreteCommandA(new ReceiverA(&cli, v[i]));
        else
            com[i] = new ConcreteCommandB(new ReceiverB(&cli, v[i]));
    }

    int k;
    pt >> k;
    vector<Invoker *> inv(k);
    for (int i = 0; i < k; i++)
    {
        int value;
        pt >> value;
        inv[i] = new Invoker(com[value]);
    }

    int m;
    pt >> m;
    for (int i = 0; i < m; i++)
    {
        int value2;
        pt >> value2;
        inv[value2]->Invoke();
        pt << cli.GetInfo();
    }

}
