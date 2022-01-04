#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XText46");
    string s1, s2;
	pt >> s1 >> s2;
	ifstream f1(s1);
	ofstream f2(s2,ios::binary);
	
    while(f1.peek() != -1){
    	double num;
    	f1 >> num;
    	if(num!=(int)num && f1.peek() != -1){
			//f2 << (char *)&num;
			f2.write((char *)&num, sizeof(num));
			//f2.put(num);
			//put(num);
			//Show(num);
		}
	}
	f1.close();
	f2.close();
}
