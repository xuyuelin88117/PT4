#include "pt4.h"
#include "mpi.h"

void Solve()
{

    Task("MPI8Inter14");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    MPI_Comm comm1;
    MPI_Comm_dup(MPI_COMM_WORLD, &comm1);
    int n;
    pt >> n;
    if (n == 0)
        n = MPI_UNDEFINED;
    MPI_Comm comm2;
    if (n == 1)
        MPI_Comm_split(MPI_COMM_WORLD, n, rank, &comm2);
    else
        MPI_Comm_split(MPI_COMM_WORLD, n, size - rank, &comm2);
    if (comm2 == MPI_COMM_NULL)
    {
        pt << -1;
        return;
    }
    int rank2;
    MPI_Comm_rank(comm2, &rank2);
    pt << rank2;
    int num = 0;
    if (n == 1)
        num = size - 1;
    MPI_Comm comm3;
    MPI_Intercomm_create(comm2, 0, comm1, num, 100, &comm3);
    int remotesize;
    MPI_Comm_remote_size(comm3, &remotesize);
    int *v1 = new int[remotesize];
    int *v2 = new int[remotesize];
    for (int i = 0; i < remotesize; i++)
        pt >> v1[i];
    MPI_Alltoall(v1, 1, MPI_INT, v2, 1, MPI_INT, comm3);
    for (int i = 0; i < remotesize; i++)
        pt << v2[i];
}