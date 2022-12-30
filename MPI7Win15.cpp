#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI7Win15");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    double *v = new double[size];
    MPI_Win window;
    for (int i = 0; i < size; i++)
        pt >> v[i];
    MPI_Win_create(v, size * 8, 8, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
    MPI_Win_fence(0, window);
    for (int i = rank + 1; i < size; i++)
        MPI_Put(&v[i], 1, MPI_DOUBLE, i, rank, 1, MPI_DOUBLE, window);
    MPI_Win_fence(0, window);
    for (int i = rank + 1; i < size; i++)
        v[i] = 0.0;
    MPI_Win_fence(0, window);
    for (int i = 0; i < size; i++)
        pt << v[i];
    MPI_Win_free(&window);
}