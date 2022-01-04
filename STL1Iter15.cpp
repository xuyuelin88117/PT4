#include "pt4.h"
#include <fstream>
#include <iostream>
#include <algorithm>
using namespace std;
typedef istream_iterator<string> is;
typedef ostream_iterator<string> os;

void Solve()
{
    Task("STL1Iter15");
    string name;
    pt >> name;
    ofstream of(name);
    typedef ptin_iterator<int> in;//
    replace_copy(in(0), in(), ostream_iterator<int>(of, "  "),0,10);
    of.close();
}
