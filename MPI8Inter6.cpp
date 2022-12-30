#include "pt4.h"
#include "mpi.h"

void Solve()
{

    Task("MPI8Inter6");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    double d1;
    pt >> d1;
    MPI_Comm comm1;
    MPI_Comm_dup(MPI_COMM_WORLD, &comm1);
    MPI_Group g;
    MPI_Comm_group(MPI_COMM_WORLD, &g);
    MPI_Status status;
    int v[1][3];
    int rank1;
    int rank2;
    int sib = 0;
    v[0][0] = 0;
    v[0][1] = size / 2 - 1;
    v[0][2] = 1;
    MPI_Group g1;
    MPI_Group_range_incl(g, 1, v, &g1);
    v[0][0] = size / 2;
    v[0][1] = size - 1;
    MPI_Group g2;
    MPI_Group_range_incl(g, 1, v, &g2);
    MPI_Comm comm2;
    if (rank < size / 2)
        MPI_Comm_create(MPI_COMM_WORLD, g1, &comm2);
    else
        MPI_Comm_create(MPI_COMM_WORLD, g2, &comm2);
    MPI_Comm_rank(comm2, &rank1);
    pt << rank1;
    if (rank < size / 2)
        sib = size / 2;
    MPI_Comm comm3;
    MPI_Intercomm_create(comm2, 0, comm1, sib, 100, &comm3);
    int flag2;
    if (rank % 2 == 0)
        flag2 = 0;
    else
        flag2 = 1;
    MPI_Comm comm4;
    if (rank < size / 2)
        MPI_Comm_split(comm3, flag2, rank, &comm4);
    else
        MPI_Comm_split(comm3, flag2, size - rank, &comm4);
    MPI_Comm_rank(comm4, &rank2);
    pt << rank2;
    MPI_Send(&d1, 1, MPI_DOUBLE, rank2, 0, comm4);
    double d2;
    MPI_Recv(&d2, 1, MPI_DOUBLE, rank2, 0, comm4, &status);
    pt << d2;
}