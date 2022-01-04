#include "pt4.h"
#include <fstream>
#include <iostream>
#include <algorithm>
using namespace std;
typedef istream_iterator<string> is;
typedef ostream_iterator<string> it;

void Solve()
{
    Task("STL1Iter11");
    string name1,name2;
    pt >> name1 >> name2;
    ifstream put(name1);
    ofstream out(name2);
    istream_iterator<int> in(put);
    ostream_iterator<int> os(out);
    istream_iterator<int> eof;
    for(in; in != eof; in++){
    	if(*in != 0){
    		os = *in;
			out << endl;
		}
	}
	put.close();
	out.close();
	
}
