#include "pt4.h"
using namespace std;

#include <iterator>
#include <vector>
#include <set>
#include <map>
#include <functional>
#include <algorithm>

typedef ptin_iterator<int> ptin;

void Solve()
{
    Task("STL5Assoc1");
    vector<int> V(ptin(0), ptin());
    bool flag = false;
    vector<int>::iterator iter = V.begin();
    vector<int>::iterator it = V.begin();
    advance(iter, V.size()/2);
    for (vector<int>::iterator p = iter; p != V.end(); ++p)
	{
		it = find(V.begin(), V.end() - V.size()/2, *iter);
	}

    if (it != V.end() - V.size()/2) {
        flag = true;
    }

    pt << flag;
}
