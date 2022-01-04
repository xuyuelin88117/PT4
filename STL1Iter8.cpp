#include "pt4.h"
#include <fstream>
#include <iostream>
#include <algorithm>
using namespace std;
typedef istream_iterator<string> is;
typedef istream_iterator<string> it;
void Solve()
{
    Task("STL1Iter8");
    string name1,name2;
    int num;
    pt >> num >> name1 >> name2;
    ifstream put(name1);
    ofstream out(name2);
    is in(put);
    is eof;
    remove_copy_if(in, eof, ostream_iterator<string>(out,"\n"),[=](string s){return s.length() > num;});
}
