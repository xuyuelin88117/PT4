#include "pt4.h"
#include "mpi.h"

void Solve()
{

    Task("MPI7Win5");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int *v = new int[size - 1];
    MPI_Win window;
    int n1, n2;
    if (rank == 0)
        for (int i = 0; i < size - 1; i++)
            pt >> v[i];
    else
    {
        pt >> n1;
        pt >> n2;
    }
    MPI_Win_create(v, (size - 1) * 4, 4, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
    MPI_Win_fence(0, window);
    if (rank != 0)
        MPI_Accumulate(&n2, 1, MPI_INT, 0, n1, 1, MPI_INT, MPI_PROD, window);
    MPI_Win_fence(0, window);
    if (rank == 0)
        for (int i = 0; i < size - 1; i++)
            pt << v[i];
    MPI_Win_free(&window);
}