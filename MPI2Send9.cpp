#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI2Send9");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    if (rank != 0) {
        int N;
        pt >> N;
        if (N != 0) {
            MPI_Send(&N, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
            for (int i = 1; i <= N; i++) {
                double a;
                pt >> a;
                MPI_Send(&a, 1, MPI_DOUBLE, 0, 0, MPI_COMM_WORLD);
            }
            MPI_Send(&rank, 1, MPI_INT, 0, 0, MPI_COMM_WORLD);
        }
    }
    else{
        int n;
        MPI_Recv(&n, 1, MPI_INT, MPI_ANY_SOURCE, 0, MPI_COMM_WORLD, MPI_STATUSES_IGNORE);
        for (int i = 1; i <= n; i++) {
            double b;
            MPI_Recv(&b, 1, MPI_DOUBLE, MPI_ANY_SOURCE, 0, MPI_COMM_WORLD, MPI_STATUSES_IGNORE);
            pt << b;
        }
        int r;
        MPI_Recv(&r, 1, MPI_INT, MPI_ANY_SOURCE, 0, MPI_COMM_WORLD, MPI_STATUSES_IGNORE);
        pt << r;
    }
}
