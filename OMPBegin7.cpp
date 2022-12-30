#include "pt4.h"
#include "omp.h"
#include <cmath>

void Solve()
{
    Task("OMPBegin7");

    int num = 0;
    double dou = 0;
    pt >> dou >> num;
    double t = omp_get_wtime();
    double final = 0;
    for (int i = 1; i <= num; i++)
    {
        double tmp = 0;
        for (int j = i; j <= num; j++)
        {
            tmp += (j + sin(dou + j)) / (2 * i * j - 1);
        }
        final += 1 / tmp;
    }
    double np_t = omp_get_wtime() - t;
    ShowLine("omp_get_wtime", np_t);
    pt << final;
    pt >> dou >> num;
    t = omp_get_wtime();
    final = 0;
#pragma omp parallel num_threads(4) reduction(+ \
                                              : final)//
    {
        int id = omp_get_thread_num();
        int pro = omp_get_num_procs();
        int thr = omp_get_num_threads();
        ShowLine("omp_get_num_procs", pro);
        ShowLine("omp_get_max_threads", thr);
        int count = 0;
        if (id == 0)
        {
            ShowLine("pro: ", pro);
            ShowLine("thr: ", thr);
        }
        double time = omp_get_wtime();
        for (int i = id; i <= num / 2; i += 4)
        {
            double tmp = 0;
            for (int j = i; j <= num; j++)
            {
                tmp += (j + sin(dou + j)) / (2 * i * j - 1);
                count++;
            }
            final += 1 / tmp;
        }
        for (int i = num - id; i > num / 2; i -= 4)
        {
            double tmp = 0;
            for (int j = i; j <= num; j++)
            {
                tmp += (j + sin(dou + j)) / (2 * i * j - 1);
                count++;
            }
            final += 1 / tmp;
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