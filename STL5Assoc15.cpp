#include "pt4.h"
using namespace std;

#include <iterator>
#include <vector>
#include <set>
#include <map>
#include <functional>
#include <algorithm>

typedef ptin_iterator<int> ptin;
typedef ptout_iterator<int> ptout;

void Solve()
{
    Task("STL5Assoc15");
    vector<int> V(ptin(0), ptin());
    sort(V.begin(), V.end());
    //auto p = V.begin();
    vector<int> vec(V.begin(), V.end());
    set<int>s(vec.begin(), vec.end());
    vec.assign(s.begin(), s.end());
    for (vector<int>::iterator it = vec.begin(); it != vec.end(); ++it)
	{
		int num = count(V.begin(),V.end(), *it);
		//Show(num);
		pt << *it;
		pt << num;
	}
    
    //copy(V.begin(), V.end(), ptout());
}
