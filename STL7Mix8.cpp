#include "pt4.h"
using namespace std;

#include <fstream>
#include <sstream>
#include <iomanip>
#include <algorithm>
#include <vector>
#include <set>
#include <map>
struct Data
{
    int code, year, month, len;
    operator string()
    {
        ostringstream os;
        os << "{ code = " << code << ", year = " << year
            << ", month = " << month << ", len = " << len << " }";
        return os.str();
    }
};

istream& operator>>(istream& is, Data& p)
{
    return is >> p.len >> p.code >> p.year >> p.month;
}
void Solve()
{
    Task("STL7Mix8");
    int k;
    pt >> k;
    string name1, name2;
    pt >> name1 >> name2;
    ifstream f1(name1.c_str());
    vector<Data> V((istream_iterator<Data>(f1)), istream_iterator<Data>());
    f1.close();
    ShowLine(V.begin(), V.end(), "V: ");

    ofstream f2(name2.c_str());
    ostream_iterator<int> out(f2, "");

    map<int, Data> m;
    int flag = 0;
	for(auto i : V)
	{
		if(k == i.code)
		{
			flag = 1;
			if(m.count(i.year))
				if(i.len < m[i.year].len)
					m[i.year] = i;
			if(m.count(i.year) == 0)
				m.insert(make_pair(i.year, i));
		}
	}
	if(flag == 0)
	{
		f2 << "No data";
	}
	multimap<int, Data,less<int>> m_res;
	for(auto i : m)
	{
		m_res.insert(make_pair(i.second.len, i.second));
	}
	/*
	for(auto it = m.begin(); it != m.end(); it++)
	{
		f2 << (*it).second.len << " " << (*it).first << " " << (*it).second.month << endl;
	}
	*/
	for(auto it = m_res.begin(); it != m_res.end(); it++)
	{
		f2 << (*it).first << " " << (*it).second.year << " " << (*it).second.month << endl;
	}
    f2.close();
}
