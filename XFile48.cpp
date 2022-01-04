#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XFile48");
fstream *f = new fstream[4];
for (int i = 0; i < 4; ++i)
{
string s;
pt >> s;
if (i < 3)
f[i].open(s, ios::binary | ios::in);
else
f[i].open(s, ios::binary | ios::out);
}
while (f[0].peek() != -1)
for (int i = 0; i < 3; ++i)
{
int x;
f[i].read((char *)&x, sizeof(x));
f[3].write((char *)&x, sizeof(x));
}
for (int i = 0; i < 4; ++i)
f[i].close();
delete[] f;
}
