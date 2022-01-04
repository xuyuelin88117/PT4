#include "pt4.h"
using namespace std;

int bit(int n)
{
	int binaryNumber = 0;
    int remainder, i = 1, step = 1;
	while (n!=0)
   {
       remainder = n%2;
       n /= 2;
       binaryNumber += remainder*i;
       i *= 10;
   }
   return binaryNumber;
}

void Solve()
{
    Task("XString25");
	string s,res;
	pt >> s;
	int a = atoi(s.c_str());
	int num = bit(a);
	res = std::to_string(num);
	pt << res;
}
