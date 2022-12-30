#include <windows.h>
#pragma hdrstop
#include "pt4.h"
#include "omp.h"
#include <cmath>

void f(double x, int from, int to, double &y, int &count)
{
	for (auto i = from; i <= to; ++i)
	{
		auto z = 0.0;
		for (auto j = 1; j <= i; ++j)
		{
			z += (j + log(1 + x + j)) / (2 * i * j - 1);
			++count;
		}
		y += 1 / z;
	}
}

void Solve()
{
	Task("OMPBegin9");

	int num = 0;
	double dou = 0, final = 0;
	pt >> dou >> num;
	double t = omp_get_wtime();
	omp_set_num_threads(1);
	final = 0.0;
	auto time = omp_get_wtime();
#pragma omp parallel
	{
		int id = omp_get_thread_num();
		auto count = 0;
		auto k = sqrt(num * num * 5 / 2 + num + 0.5) - num;
#pragma omp sections reduction(+ \
							   : final)
		{
#pragma omp section
			{
				f(dou, 1, k, final, count);
			}
#pragma omp section
			{
				f(dou, k + 1, num, final, count);
			}
		}
		time = omp_get_wtime() - time;
#pragma omp critical
		{
			ShowLine("omp_get_thread_num", id);
			ShowLine("count", count);
			ShowLine("thread_time", 1000 * time);
		}
	}
	double np_t = omp_get_wtime() - t;
	ShowLine("omp_get_wtime", np_t);
	pt << final;
	int thr = omp_get_max_threads();
    int pro = omp_get_num_procs();
    ShowLine("omp_get_num_procs", pro);
    ShowLine("omp_get_max_threads", thr);
	pt >> dou >> num;
	omp_set_num_threads(2);
	t = omp_get_wtime();
	final = 0.0;
#pragma omp parallel
	{
		auto count = 0;
		auto k = sqrt(num * num * 5 / 2 + num + 0.5) - num;
#pragma omp sections reduction(+ \
							   : final)
		{
#pragma omp section
			{
				f(dou, 1, k, final, count);
			}
#pragma omp section
			{
				f(dou, k + 1, num, final, count);
			}
		}
	}
	double par_t = omp_get_wtime() - t;
    ShowLine("omp_get_wtime", par_t);
    ShowLine("rate", np_t / par_t);
	pt << final;//
}
