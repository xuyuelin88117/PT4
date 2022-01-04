#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XText49");
    string s1, s2, s = "tmp.tst";
	pt >> s1 >> s2;
	ifstream f1(s1);
	ofstream f(s);
	ifstream f2(s2, ios::binary);
	while (f1.peek() != -1 && f2.peek() != -1)
	{
		string s;
		getline(f1, s);
		int num;
		f2.read((char *)&num, sizeof(num));
		//Show(num);
		f << s << num << endl;
		//fprintf(f,"%d",num);
	}
	while (f1.peek() != -1)
	{
		string s;
		getline(f1, s);
		f << s<< endl;
		//fprintf(f,"%d",num);
	}
	f1.close();
	f2.close();
	f.close();
	remove(s1.c_str());
	rename(s.c_str(), s1.c_str());
}
