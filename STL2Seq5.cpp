#include <windows.h>
#pragma hdrstop
#include "pt4.h"
using namespace std;
#include <iterator>
#include <vector>
#include <deque>
#include <list>
typedef ptin_iterator<int> ptin;
typedef ptout_iterator<int> ptout;

void Solve()
{
    Task("STL2Seq5");
    list<int> L(ptin(0), ptin());
    list<int>::iterator p = L.begin();
    list<int>::reverse_iterator q = L.rbegin();
    list<int>::reverse_iterator r(L.end());
    advance(p, L.size() / 3);
    L.push_back(*p);
	Show(L.begin(), p, "L: ");
	copy(L.begin(), p, ptout());

	advance(q, L.size() / 3);
	advance(r, L.size() / 3);
	advance(r, L.size() / 3);
	copy(++q, ++r, ptout());
	
	q = L.rbegin();
    r = L.rbegin();
	advance(r, L.size() / 3);
	copy(++q, ++r, ptout());
	
}
