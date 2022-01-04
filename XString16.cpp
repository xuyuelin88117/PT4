#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString16");
    string s,s1;
	pt >> s;
	char c;
	int i = 0;
	while (s[i])
    {
        c = tolower(s[i]);
        s1 = s1 + c;
        i++;
    }
    pt << s1;
}
