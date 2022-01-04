#include "pt4.h"
using namespace std;

#include <algorithm>
#include <iterator>
#include <string>

char op(char str)
{
   if(str >= 'A' && str <= 'Z'){

			return tolower(str);
		}

	if(str >= 'a' && str <= 'z'){
		//Show(str);
		return  toupper(str);
	}

	if(str >= '0' && str <= '9')
		return '*';
}
void Solve()
{
    Task("STL4Str23");
    string input, output;
    pt >> input;
    output.resize(input.size());
	transform(input.begin(),input.end(),output.begin(),op);
    pt << output;
}
