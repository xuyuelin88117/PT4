#include "pt4.h"
#include <fstream>
#include <cmath>
#include <iomanip>
using namespace std;

void Solve()
{
    Task("XText42");
    double a, b, step_size;
	int N;
	string name;
    pt >> a >> b >> N >> name;
    ofstream f(name);
    step_size = (b - a)/N;
    f<<fixed;
    for(int i = 0; i <= N; i ++){
    	f << setw(10) << setprecision(4) << a + i * step_size << setw(15)  << setprecision(8) << sqrt(a + i * step_size)<< endl;
    	//f << a + i * step_size << sqrt(a + i * step_size) << endl;
	}
    f.close();
}
