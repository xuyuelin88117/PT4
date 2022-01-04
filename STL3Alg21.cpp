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

void Solve()
{
    Task("STL3Alg21");
    int k;
    pt >> k;
    list<int> L1(ptin(0), ptin());
    list<int> L2(ptin(0), ptin());
    auto p = L1.rbegin();
    advance(p, k);
    rotate(L1.rbegin(), p, L1.rend());
    auto p2 = L2.begin();
    advance(p2, k);
    rotate(L2.begin(), p2, L2.end());
    Show(L1.begin(), L1.end(), "L1: ");
    Show(L2.begin(), L2.end(), "L2: ");
    copy(L1.begin(), L1.end(), ptout());
    copy(L2.begin(), L2.end(), ptout());
}
