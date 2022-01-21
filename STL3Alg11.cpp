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
    Task("STL3Alg11");
    vector<int> V(ptin(0), ptin());
    deque<int>  D(ptin(0), ptin());
    deque<int>  D_1(D.size()/2);
    for(auto i : D){
    	
	}
    Show(V.begin(), V.end(), "V: ");
    //copy(V.begin(), V.end(), ptout());
}
