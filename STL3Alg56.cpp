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
    Task("STL3Alg56");
    vector<int> V(ptin(0), ptin());
    int sum1 = accumulate(V.begin(), V.end(), 0, [](int sum, int m) {if (m < 0)return sum + m; return sum; });
    int sum2 = accumulate(V.begin(), V.end(), 0, [](int sum, int m) {if (m > 0)return sum + m; return sum; });
    pt << sum1 << sum2;
    //copy(Vresult.begin(), Vresult.end(), ptout());
}
