#include "pt4.h"
using namespace std;
#include <fstream>

void Solve()
{
    Task("XFile34");
    string s1, s2 = "$F34$.tmp";
	pt >> s1;
	ifstream f1(s1, ios::binary);
	ofstream f2(s2, ios::binary);
	while (f1.peek() != -1)
	{
		int x;
		f1.read((char *)&x, sizeof(x));
		if(x >= 0)
		f2.write((char *)&x, sizeof(x));
	}
	f1.close();
	f2.close();
	remove(s1.c_str());
	rename(s2.c_str(), s1.c_str());
}
