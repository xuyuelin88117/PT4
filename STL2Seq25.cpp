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
    Task("STL2Seq25");
    list<int>  L1(ptin(0), ptin());
    list<int>  L2(ptin(0), ptin());
    list<int>::iterator it = L2.begin();
    advance(it, L2.size()/2);
    list<int> L(L2.begin(),it);
    L1.splice(L1.begin(), L);
    list<int> LL(it,L2.end());
    L2.swap(LL);
    //L2.splice(L2.begin(), L1, it);
    Show(L1.begin(), L1.end(), "L1: ");
    Show(L2.begin(), L2.end(), "L2: ");
    copy(L1.begin(), L1.end(), ptout());
    copy(L2.begin(), L2.end(), ptout());
}
