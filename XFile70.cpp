#include "pt4.h"
#include <fstream>
using namespace std;

string dat(char s[80]){
	string r = s;
	return r.substr(3,2);
	//return r.substr(6,4)+r.substr(3,2)+r.substr(0,2);
}
void Solve()
{
    Task("XFile70");
    string s1, s2;
	char s[80][100];
	pt >> s1 >> s2;
	ifstream f1(s1, ios::binary);
	ofstream f2(s2, ios::binary);
	int n = 0;
	while (f1.peek() != -1)
	{
		f1.read(s[n],80);
		n++;
	}
	for(int k = n-1 ; k>= 0; k --){
		//for(int i = 0; i < n - 1; i ++){
			string month = dat(s[k]);
			if(month == "02" || month == "12" || month == "01"){
				f2.write(s[k],80);
			}
		//}
	}
	f1.close();
	f2.close();
}

