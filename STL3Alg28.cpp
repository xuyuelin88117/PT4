#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <vector>
#include <deque>
#include <list>
#include <functional>
typedef ptin_iterator<int> ptin;
typedef ptout_iterator<int> ptout;

bool bools(int i) {
	return (i < 0);
}

void Solve()
{
    Task("STL3Alg28");
    list<int> L(ptin(0), ptin());
    int num = 0;
    auto p = L.begin();
    advance(p, L.size() / 2);
    for (list<int>::iterator it = p; it != L.end(); ++it)
	{
		if(*it > 0)
			num ++;
	}
    list<int> L1(num);
    remove_copy_if(p, L.end(), L1.begin(), bools);
    list<int>::iterator iter = L.end();
    for (list<int>::iterator it = L.begin(); it != L.end(); ++it)
	{
		inserter(L1, L1.end()) = *it;
	}
    Show(L1.begin(), L1.end(), "L: ");
    copy(L1.begin(), L1.end(), ptout());
}
