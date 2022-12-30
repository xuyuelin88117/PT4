#include "pt4.h"
#include "mpi.h"

void Solve()
{

    Task("MPI8Inter7");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    MPI_Comm comm;
    MPI_Comm_dup(MPI_COMM_WORLD, &comm);
    int n;
    int panduan;
    pt >> n;
    if (rank < size / 2)
    {
        panduan = 0;
    }
    else
    {
        panduan = 1;
    }
    MPI_Comm comm1;
    MPI_Comm_split(MPI_COMM_WORLD, panduan, rank, &comm1);
    int rank2;
    MPI_Comm_rank(comm1, &rank2);
    pt << rank2;
    int panding = 0;
    if (rank < size / 2)
        panding = size / 2;
    MPI_Comm comm2;
    MPI_Intercomm_create(comm1, 0, comm, panding, 100, &comm2);
    int size2;
    MPI_Comm_remote_size(comm2, &size2);
    MPI_Comm comm3;
    MPI_Comm_split(comm2, n, size - rank - 1, &comm3);
    int rank_2, size_2, remotesize;
    MPI_Comm_rank(comm3, &rank_2);
    MPI_Comm_size(comm3, &size_2);
    MPI_Comm_remote_size(comm3, &remotesize);
    int *v = new int[remotesize];
    MPI_Status status;
    int num;
    if (size_2 > 1 && n == 1)
    {
        pt << rank_2;
        pt >> num;
        MPI_Send(&num, 1, MPI_INT, 0, 0, comm3);
        MPI_Recv(&num, 1, MPI_INT, 0, 0, comm3, &status);
        pt << num;
    }
    if (size_2 == 1 && n == 1)
    {
        for (int i = 0; i < remotesize; i++)
        {
            pt >> v[i];
            MPI_Recv(&num, 1, MPI_INT, i, 0, comm3, &status);
            pt << num;
            MPI_Send(&v[i], 1, MPI_INT, i, 0, comm3);
        }
    }
}