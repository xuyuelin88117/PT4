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
    Task("STL3Alg30");
    vector<int> V(ptin(0), ptin());
    auto p = V.begin();
    advance(p, V.size() / 2);
    V.erase(remove(p,V.end(),0),V.end());
    Show(V.begin(), V.end(), "V: ");
    copy(V.begin(), V.end(), ptout());
}
