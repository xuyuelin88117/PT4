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
    Task("STL3Alg2");
    deque<int> D(ptin(0), ptin());
    auto it = find(D.rbegin(), D.rend(), 0);
    if (it != D.rend())
        D.erase(--it.base());
    copy(D.begin(), D.end(), ptout());
}
