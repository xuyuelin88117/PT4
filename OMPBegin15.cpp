#include "pt4.h"
#include "omp.h"
#include <cmath>

void Solve()
{
	Task("OMPBegin15");
	double x;
	int n;
	pt >> x >> n;
	double t = omp_get_wtime();
	double final = 0;
	for (int i = 1; i <= n; i++)
	{
		double nonsum = 0;
		for (int j = i; j <= n; j++)
		{
			nonsum += (j + pow(x + j, 0.25)) / (2.0 * i * j - 1);
		}
		final += 1 / nonsum;
	}
	pt << final;
	double np_t = omp_get_wtime() - t;
	ShowLine("Non-parallel np_t:", np_t);
	pt >> x >> n;
	final = 0;
	omp_set_num_threads(4);
	int thr = omp_get_max_threads();
	int pro = omp_get_num_procs();
	ShowLine("num_procs: ", pro);
	ShowLine("num_threads: ", thr);
	int num = (n + 1) * n / 2;
	int cou = 1;
	int tmp;
	for (tmp = 0; tmp <= num / 4; cou++)
		for (int j = cou; j <= n; j++)
			tmp++;
	int num1 = cou;
	for (tmp = 0; tmp <= num / 4; ++cou)
		for (int j = cou; j <= n; j++)
			tmp++;
	int num2 = cou;
	for (tmp = 0; tmp <= num / 4; ++cou)
		for (int j = cou; j <= n; ++j)
			tmp++;
	int num3 = cou;
#pragma omp parallel reduction(+ \
							   : final)
	{
		int id = omp_get_thread_num();
		double time = omp_get_wtime();
		int count = 0;
		if (id == 0)
		{
			for (int i = 0; i <= num1; ++i)
			{
				double sum = 0;
				for (int j = i; j <= n; ++j)
				{
					count++;
					sum += (j + pow(x + j, 0.25)) / (2.0 * i * j - 1);
				}
				final += 1 / sum;
			}
		}
		if (id == 1)
		{
			for (int i = num1 + 1; i <= num2; ++i)
			{
				double sum = 0;
				for (int j = i; j <= n; ++j)
				{
					count++;
					sum += (j + pow(x + j, 0.25)) / (2.0 * i * j - 1);
				}
				final += 1 / sum;
			}
		}
		if (id == 2)
		{
			for (int i = num2 + 1; i <= num3; ++i)
			{
				double sum = 0;
				for (int j = i; j <= n; ++j)
				{
					count++;
					sum += (j + pow(x + j, 0.25)) / (2.0 * i * j - 1);
				}
				final += 1 / sum;
			}
		}
		if (id == 3)
		{
			for (int i = num3 + 1; i <= n; ++i)
			{
				double sum = 0;
				for (int j = i; j <= n; ++j)
				{
					count++;
					sum += (j + pow(x + j, 0.25)) / (2.0 * i * j - 1);
				}
				final += 1 / sum;
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
	double par_t = omp_get_wtime() - t;
	ShowLine("omp_get_wtime", par_t);
	ShowLine("rate", np_t / par_t);
	pt << final;
}
