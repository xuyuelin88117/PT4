#include "mpi.h"
#include "pt4.h"

void Solve()
{

    Task("MPI8Inter1");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int n1;
    int size2 = size / 2;
    pt >> n1;
    MPI_Comm comm1;
    MPI_Comm_dup(MPI_COMM_WORLD, &comm1);
    MPI_Group g1;
    MPI_Comm_group(MPI_COMM_WORLD, &g1);
    int *v1 = new int[size2];
    for (int i = 0; i < size2; i++)
        v1[i] = 2 * i;
    MPI_Group g2;
    MPI_Group_incl(g1, size2, v1, &g2);
    int *v2 = new int[size2];
    for (int i = 0; i < size2; i++)
        v2[i] = v1[i] + 1;
    MPI_Group g3;
    MPI_Group_incl(g1, size2, v2, &g3);
    MPI_Comm comm2;
    if (rank % 2 == 0)
    {
        MPI_Comm_create(MPI_COMM_WORLD, g2, &comm2);
    }
    else
    {
        MPI_Comm_create(MPI_COMM_WORLD, g3, &comm2);
    }
    int r1;
    MPI_Comm_rank(comm2, &r1);
    pt << r1;
    int m = 0;
    if (rank % 2 == 0)
        m = 1;
    MPI_Comm comm3;
    MPI_Intercomm_create(comm2, 0, comm1, m, 100, &comm3);
    MPI_Send(&n1, 1, MPI_INT, r1, 0, comm3);
    int r2;
    MPI_Status status;
    MPI_Recv(&r2, 1, MPI_INT, r1, 0, comm3, &status);
    pt << r2;
}