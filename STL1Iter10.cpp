#include "pt4.h"
using namespace std;

bool bools(int i) {
//ȡһ��ֵ�ܱ���������ֵ
	return ((i % 2) == 1);
}

void Solve()
{
    Task("STL1Iter10");
    typedef ptin_iterator<double> in;
    typedef ptout_iterator<double> out;
    remove_copy_if(in(0), in(), out, bools);
}
