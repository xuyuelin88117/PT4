#include "pt4.h"
#include "omp.h"
#include <cmath>
#include <string>

using namespace std;

void Solve()
{
    Task("OMPBegin2");

    int num = 0;
    double dou = 0;
    pt >> dou >> num;
    double t = omp_get_wtime();
    double rez = 0.0;
    double sum;
    for (int i = 1; i <= num; i++)
    {
        sum = 0.0;
        for (int j = 1; j <= i + num; j++)
            sum += (j + pow(dou + j, 1.0 / 3.0)) / (2 * i * j - 1);
        rez += 1 / sum;
    }
    double np_t = omp_get_wtime() - t;
    ShowLine("omp_get_wtime", np_t);
    pt << rez;
    pt >> dou >> num;
    omp_set_num_threads(2);
    t = omp_get_wtime();
    double final = 0.0;
    int thr = omp_get_max_threads();
    int pro = omp_get_num_procs();
    ShowLine("omp_get_num_procs", pro);
    ShowLine("omp_get_max_threads", thr);
#pragma omp parallel private(sum) shared(dou, num) reduction(+ \
                                                             : final)
    {
        int id = omp_get_thread_num();
        int count = 0;
        double time = omp_get_wtime();
        for (int i = id + 1; i <= num; i = i + 2)
        {
            sum = 0.0;
            for (int j = 1; j <= i + num; j++)
            {
                sum += (j + pow(dou + j, 1.0 / 3.0)) / (2 * i * j - 1);
                count++;//
            }
            final += 1 / sum;
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