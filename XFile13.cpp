#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XFile13");
    string s1, s2 , s3;
	pt >> s1 >> s2 >> s3;
	ifstream f1(s1, ios::binary);
	ofstream f2(s2, ios::binary);
	ofstream f3(s3, ios::binary);
	f1.seekg(-4, ios::end);
	while (f1.peek() != -1)
	{
		int x;
		f1.seekg(f1.tellg());
		f1.read((char *)&x, sizeof(x));
		f1.seekg(-8, ios::cur);
		if(x > 0)
		f2.write((char *)&x, sizeof(x));
		if(x < 0)
		f3.write((char *)&x, sizeof(x));
	}
	f1.close();
	f2.close();
	f3.close();
}

