#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI7Win8");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int n;
    MPI_Win windows;
    int typesize;
    double d, res;
    pt >> n >> d;
    MPI_Type_size(MPI_DOUBLE, &typesize);
    MPI_Win_create(&res, typesize, typesize, MPI_INFO_NULL, MPI_COMM_WORLD, &windows);
    MPI_Win_fence(0, windows);
    MPI_Put(&d, 1, MPI_DOUBLE, n, 0, 1, MPI_DOUBLE, windows);
    MPI_Win_fence(0, windows);
    pt << res;
    MPI_Win_free(&windows);
}
