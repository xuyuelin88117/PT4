#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString30");
    char C;
	string s,s0;
	pt >> C >> s >>s0;
    int position = 0;

    //position=s.find(C);
    //s.insert(position+1,s0);

	while(position <= s.rfind(C))
    {
    	position=s.find(C,position);
        //cout<<"position  "<<i<<" : "<<position<<endl;
        s.insert(position+1,s0);
        position++;
    }

    


    pt << s;
}
