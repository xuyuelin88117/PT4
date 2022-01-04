#include "pt4.h"
using namespace std;

void Solve()
{
    Task("XString64");
    string s;
    int K;
    pt >> s >> K;
    for(int i = 0; i < s.length(); i ++){
    	if(s[i] >= 'a' && s[i] <= 'z'){
			if(s[i] - 'a' < K){
				s[i] += 26;
			}
			s[i] = s[i] - K;
		}
		if(s[i] >= 'A' && s[i] <= 'Z'){
			if(s[i] - 'A' < K){
				s[i] = s[i] + 26;
			}
    		s[i] = s[i] - K;
		}
	}
    pt << s;
}
