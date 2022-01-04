#include "pt4.h"
using namespace std;

#include <iterator>
#include <vector>
#include <set>
#include <map>
#include <functional>
#include <algorithm>

typedef ptin_iterator<string> ptin;

void Solve()
{
    Task("STL5Assoc22");
    vector<string> V(ptin(0), ptin());
    multimap<char, string> M;
    reverse(V.begin(), V.end());
    for (auto e : V)
    {
        M.insert(make_pair(e[0], e));
    }
    pt << M;
}
