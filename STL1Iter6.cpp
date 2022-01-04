#include "pt4.h"
#include <fstream>
#include <iostream>
#include <algorithm>
using namespace std;

void Solve()
{
	Task("STL1Iter6");
	string name;
	pt >> name;
	ofstream f(name);
	int num;
	pt >> num;

	char arr[num];

	for (int i = 0; i < num; i++)
		pt >> arr[i];

	ostream_iterator<char> whatever(f, " ");
	copy(arr, arr + num, whatever);
}
