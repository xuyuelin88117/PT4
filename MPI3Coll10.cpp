#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI3Coll10");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    const int sumsize = size + 2;
    double *dou = (double *)malloc(sizeof(double) * (size + 2));
    double *lodou = (double *)malloc(sizeof(double) * 3);
    int *in = (int *)malloc(sizeof(int) * size);
    int *num = (int *)malloc(size * sizeof(int));

    if (dou == NULL || lodou == NULL || in == NULL || num == NULL)
    {
        printf("error\n");
        exit(-1);
    }
    
    if (rank == 0)
    {
        for (int i = 0; i < size + 2; i++)
        {
            pt >> dou[i];
        }
        for (int i = 0; i < size; i++)
        {
            in[i] = 3;
            num[i] = i;
        }
    }

    MPI_Scatterv(dou, in, num, MPI_DOUBLE, lodou, 3, MPI_DOUBLE, 0, MPI_COMM_WORLD);
    free(in);
    free(num);
    free(dou);

    for (int i = 0; i < 3; i++)
    {
        pt << lodou[i];
    }
    free(lodou);
}
