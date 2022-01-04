#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XFile55");
    int N;
    string s0,s;
    pt >> s0;
	pt >> N;
	fstream* f = new fstream[N+1];
	for(int i = 0; i < N; ++i){
		pt >> s;
		f[i].open(s, ios::binary | ios::in);
	}
	f[N].open(s0, ios::binary | ios::out);
	///////////////////////////////////////////
	for(int i = 0; i < N; ++i){
		f[i].seekg(0);
		f[i].seekg(f[i].tellg());
		int num = 0;
		while (f[i].peek() != -1)
		{
			int x;
			f[i].read((char *)&x, sizeof(x));
			num++;
		}
		f[N].write((char *)&num, sizeof(num));
		num = 0;
		f[i].seekg(0);
		f[i].seekg(f[i].tellg());
		while (f[i].peek() != -1)
		{
			int x;
			f[i].read((char *)&x, sizeof(x));
			f[N].write((char *)&x, sizeof(x));
		}
	}
	
	///////////////////////////////////
	for (int i = 0; i <= N; ++i)
		f[i].close();
	delete[] f;
}

