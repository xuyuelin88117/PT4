#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString8");
    int N;
	char C;
	pt >> N;
	pt >> C;
	string s;
	for(int i = 0; i < N; i ++){
		s = s + C;
	}
    pt << s;
}
