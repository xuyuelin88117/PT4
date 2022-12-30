#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI5Comm1");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    
    MPI_Group ishgrup, novgrup;
    MPI_Comm novcomm;

    int K = (size + 1) / 2;
    int* ranks = new int[K];

    for (int i = 0; i < K; i++)
        ranks[i] = i * 2;

    MPI_Comm_group(MPI_COMM_WORLD, &ishgrup);
    MPI_Group_incl(ishgrup, K, ranks, &novgrup);
    MPI_Comm_create(MPI_COMM_WORLD, novgrup, &novcomm);
    int* a = new int[K];
    int b;

    if (rank == 0) {
        for (int i = 0; i < K; i++) {
            pt >> a[i];
        }
    }
    MPI_Scatter(a, 1, MPI_INT, &b, 1, MPI_INT, 0, novcomm);

    if (rank % 2 == 0 || rank == 0) {
        pt << b;
    }
}
