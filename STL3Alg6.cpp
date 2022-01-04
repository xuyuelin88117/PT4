#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <vector>
#include <deque>
#include <list>
#include <functional>
#include <string>
typedef ptin_iterator<int> ptin;
typedef ptout_iterator<int> ptout;

void Solve()
{
    Task("STL3Alg6");
    vector<int> V(ptin(0), ptin());
    list<int>   L(ptin(0), ptin());
    auto p = V.begin();
    advance(p, V.size() / 2);
    list<int> L1(L.rbegin(), L.rend());
    auto pos = find_first_of(L1.begin(), L1.end(), V.begin(), p);
    if (pos != L1.end())
        L1.insert(pos, *pos);
    list<int> L2(L1.rbegin(), L1.rend());
    copy(L2.begin(), L2.end(), ptout());
}
