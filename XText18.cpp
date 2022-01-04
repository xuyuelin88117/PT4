#include "pt4.h"
#include <fstream>
using namespace std;

void Solve()
{
    Task("XText18");
    int k;
    string s1,s2 = "tmp.txt";
    pt >> k >> s1;
    ifstream f1(s1, ios::binary);
	ofstream f2(s2, ios::binary);
	for(int i = 0; i < k; i ++){
		char c;
		f1.read((char *)&c, sizeof(c));
		if(c == '\n'){
			f2.write((char *)&c, sizeof(c));
			//f1.seekg(k, ios::cur);
			f1.seekg(k, ios::cur);
			f1.seekg(f1.tellg());
			break;
		}
	}
	while (f1.peek() != -1)
	{
		char c;
		f1.read((char *)&c, sizeof(c));
		f2.write((char *)&c, sizeof(c));
		if(c == '\n'){
			
			for(int i = 0; i < k; i ++){
				f1.read((char *)&c, sizeof(c));
				
				if(c == '\n' && f1.peek() != -1){
					f2.write((char *)&c, sizeof(c));
					f1.seekg(k, ios::cur);
					f1.seekg(f1.tellg());
					break;
				}

				
			}
			
		}
	}
	f1.close();
	f2.close();
	remove(s1.c_str());
	rename(s2.c_str(), s1.c_str());
}
