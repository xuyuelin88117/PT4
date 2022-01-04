#include "pt4.h"
#include <fstream>
#include <algorithm>
using namespace std;
void Solve()
{
    Task("STL1Iter2");
    string name;
    pt >> name;
    ifstream f(name);
    istream_iterator<double> whatever(f), eof;
    int n = count_if(whatever, eof, [](double a) {return a > 0; });
    pt << n;
    f.close();
}
