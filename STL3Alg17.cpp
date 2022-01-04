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
    Task("STL3Alg17");
    int a, b;
    pt >> a >> b;
    vector<int> V1(ptin(0), ptin());
    vector<int> V2(ptin(0), ptin());
    fill_n(inserter(V1,V1.begin()),5,a);
    fill_n(back_inserter(V1),5,b);
	V2.insert(V2.begin(),5,a);
	V2.insert(V2.end(),5,b);

    copy(V1.begin(), V1.end(), ptout());
    copy(V2.begin(), V2.end(), ptout());
}
