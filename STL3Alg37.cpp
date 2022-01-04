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
    Task("STL3Alg37");
    vector<int> V(ptin(0), ptin());
    size_t count {3};
	vector<int> result(count); // Destination for the results һ count elements
	partial_sort_copy(begin(V), end(V), begin (result) , end (result));
	//copy(std::begin(numbers), std::end(numbers), std::ostream_iterator<int>{std::cout," " });
    Show(result.begin(), result.end(), "V: ");
    copy(result.begin(), result.end(), ptout());
}
