#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XText36");
    string name1, name2 = "tmp.tst", s;
    pt >> name1;
    ifstream f1(name1);
    ofstream f2(name2);
    int num;
    while (f1.peek() != -1)
    {
        getline(f1, s);
        num = s.find_first_not_of(' ');
        if( num % 2 == 1){
        	s.erase(0, num / 2 + 1);
		}
		if( num % 2 == 0){
        	s.erase(0, num / 2);
		}
		f2 << s << endl;
    }
    f1.close();
    f2.close();
    remove(name1.c_str());
    rename(name2.c_str(), name1.c_str());
}
