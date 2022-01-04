#include "pt4.h"
#include <fstream>
#include <algorithm>
#include <string>
using namespace std;

void Solve()
{
	Task("XText55");
	string s1, s2;
	pt >> s1 >> s2;
	ifstream f1(s1);
	ofstream f2(s2, ios::binary);
	string s;
	char c;
	while(f1){
		c = f1.get();
		if(isalnum(c) || ispunct(c) || c == ' '){
				s = s + c;
		}
	}
	sort(s.begin(), s.end());
	for(int i = 1; i < s.length();){
		if(s[i] == s[i -1]){
			s.erase(i,1);
		}else
			i++;
	}
	f2 << s;
	f1.close();
	f2.close();
}
