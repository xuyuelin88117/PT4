#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <vector>
#include <deque>
#include <list>
#include <functional>
typedef ptin_iterator<string> ptin;
typedef ptout_iterator<string> ptout;

bool length(string s1, string s2)
{
	if(s1.length() > s2.length()) return true;
	else if(s1.length() == s2.length())
	{
		if(s1 <= s2) return true;
	}
	else return false;
}

void Solve()
{
    Task("STL3Alg46");
    deque<string> D(ptin(0), ptin());
    sort(D.begin(), D.end(), length);
    Show(D.begin(), D.end(), "D: ");
    copy(D.begin(), D.end(), ptout());
}
