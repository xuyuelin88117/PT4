#include "pt4.h"
using namespace std;

#include <iterator>
#include <vector>
#include <set>
#include <map>
#include <functional>
#include <algorithm>

typedef ptin_iterator<string> ptin;
typedef ptout_iterator<string> ptout;

void Solve()
{
    Task("STL5Assoc32");
    vector<string> V1(ptin(0), ptin());
    vector<string> V2(ptin(0), ptin());
    
    sort(V1.begin(), V1.end());
    reverse(V2.begin(), V2.end());
    //Show(V1);
    //Show(V2);
    
    multimap<string, string> V;
    int flag = 0;

    for(auto i : V1)
	{
		for(auto j : V2)
		{
			if(i.at(0) == j.at(j.length()-1))
			{
				//p.first = i;
				//p.second = j;
				//Show(p);
				V.insert(pair<string, string>(i, j));
				
				//Show(j);
				flag=1;
			}
		}
		flag = 0;
	}
	Show(V);
	pt << V.size();
	for(auto it = V.begin(); it != V.end(); it++)
	{
		pt << (*it).first << (*it).second;
		//copy((*it).second.begin(), (*it).second.end(), ptout());
	}
}
