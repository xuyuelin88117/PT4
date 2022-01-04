#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString40");
    string s;
    pt >> s;
    int position_1 = s.find(" ");
    int position_2 = s.rfind(" ");
    s.erase(s.begin() + position_2, s.end());
    s.erase(0, position_1+1);
    pt << s;
}
