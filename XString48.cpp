#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString48");
    string s;
    pt >> s;
    s = s + ' ';
    int m;
    int p;
    m = s.find_first_not_of(" ");
    p = s.find(" ");
    for (int i = m+1; i <= p - 1; i++)
    {
        if (s[i] == s[m] && i != m)
        {
            s[i] = '.';
        }
        if (i == p - 1)
        {
            m = s.find_first_not_of(" ", p+1);
            p = s.find(" ", m);
        }
    }
    int j = s.rfind(" ");
    s[j] = '\0';
    pt << s;
}
