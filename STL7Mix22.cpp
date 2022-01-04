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
    int num, year;
    string fam;
    operator string()
    {
        ostringstream os;
        os << "{ num = " << num << ", year = " << year
            << ", fam = " << fam << " }";
        return os.str();
    }
};

istream& operator>>(istream& is, Data& p)
{
    return is >> p.fam >> p.num >> p.year;
}
void Solve()
{
    Task("STL7Mix22");
    string name1, name2;
    pt >> name1 >> name2;
    ifstream f1(name1.c_str());
    vector<Data> V((istream_iterator<Data>(f1)), istream_iterator<Data>());
    f1.close();
    ShowLine(V.begin(), V.end(), "V: ");

    ofstream f2(name2.c_str());
    
    map<int, set<int>> M;
    for (auto& it : V)
        M[it.num].insert(it.year);
    for (auto& it : M) {
        f2 << it.first;
        for (auto n : it.second)
            f2 << " " << n;
        f2 << endl;
    }

    f2.close();
}
