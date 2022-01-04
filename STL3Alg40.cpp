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

class mycomp2 {
public:
    bool operator()(const int& i) {
        return (i%2 == 0);
    }
};

void Solve()
{
    Task("STL3Alg40");
	list<int> L(ptin(0), ptin());
	list<int> Lresult;
    auto bound = partition(L.begin(), L.end(), mycomp2());
    
    Lresult.push_back(distance(L.begin(), bound));
    Lresult.push_back(distance(bound, L.end()));
    Show(Lresult.begin(), Lresult.end(), "Lresult: ");
    copy(Lresult.begin(), Lresult.end(), ptout());
}
