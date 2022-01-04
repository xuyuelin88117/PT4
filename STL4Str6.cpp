#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <string>

void Solve()
{
    Task("STL4Str6");
    int k;
    string s1,s2;
    pt >> k >> s1 >> s2;
    s2.insert(1,s1,0,k);
    reverse(s1.begin(), s1.end());
    s2.insert(s2.length()-1,s1,0,k);
    pt << s2;
}
