#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString59");
    string s;
    pt >> s;
    int position = s.rfind('.');
    s.erase(0,position+1);
    pt << s;
}
