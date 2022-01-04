#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <string>

void Solve()
{
    Task("STL4Str20");
    int n;
    string s;
    pt >> n >> s;
    string str;
    for(int i = 0; i < n; i ++){
    	str += 'A' + i;
	}
	s.insert(s.length(),str);
	reverse(str.begin(), str.end());
	s.insert(0,str);
    pt << s;
}
