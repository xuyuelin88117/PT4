#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI2Send17");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    int a;
    MPI_Status s;
    if (rank == 0) {
        for (int i = 1; i < size; i++) {
            pt >> a;
            MPI_Ssend(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
            MPI_Recv(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD, &s);
            pt << a;
        }
    }
    else if (rank < size-1) {
        for (int i = 0; i < rank; i++) {
            int b;
            MPI_Recv(&b, 1, MPI_INT, i, 0, MPI_COMM_WORLD, &s);
            pt << b;
            pt >> a;
            MPI_Ssend(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
        }
        for (int i = rank + 1; i <= size - 1; i++) {
            pt >> a;
            MPI_Ssend(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
            MPI_Recv(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD, &s);
            pt << a;
        }
    }
    else {
        for (int i = 0; i < size -1; i++) {
            int b;
            MPI_Recv(&b, 1, MPI_INT, i, 0, MPI_COMM_WORLD, &s);
            pt << b;
            pt >> a;
            MPI_Ssend(&a, 1, MPI_INT, i, 0, MPI_COMM_WORLD);
        }
    }
}
