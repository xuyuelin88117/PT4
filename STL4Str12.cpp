#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <string>

void Solve()
{
    Task("STL4Str12");
    string s,str;
    pt >> s;
    auto it = s.begin();
    int n = s.size();
    for (int i = 1; i <= n; i++)
    {
        if (i % 3)
            it++;
        else{
        	//s.insert(*it, s.length());
        	//s += *it;
        	str += *it;
            it = s.erase(it);
		}
    }
    s.insert(s.length(),str);
    pt << s;

}
