#include "pt4.h"
#include "mpi.h"

#include <cmath>

int k; // number of processes
int r; // rank of the current process

int m, p, q;    // sizes of the given matrices
int m0, p0, q0; // sizes of the matrix blocks
int k0;         // order of the Cartesian grid (equal to sqrt(k))

int *a_, *b_, *c_;  // arrays to store matrices in the master process
int *a, *b, *c, *t; // arrays to store matrix blocks in each process

MPI_Datatype MPI_BLOCK_A; // datatype for the block of the matrix A
MPI_Datatype MPI_BLOCK_B; // datatype for the block of the matrix B
MPI_Datatype MPI_BLOCK_C; // datatype for the block of the matrix C

MPI_Comm MPI_COMM_GRID = MPI_COMM_NULL;
// communicator associated with a two-dimensional Cartesian grid
MPI_Comm MPI_COMM_ROW = MPI_COMM_NULL;
// communicator associated with rows of a two-dimensional grid
MPI_Comm MPI_COMM_COL = MPI_COMM_NULL;
// communicator associated with columns of a two-dimensional grid

void Solve()
{
    Task("MPI9Matr34");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    k = size;
    r = rank;
    k0 = (int)floor(sqrt((double)k) + 0.1);

    pt >> p0 >> q0;
    b_ = new int[p0 * q0];
    for (int i = 0; i < p0 * q0; i++)
    {
        pt >> b_[i];
    }
    int v1[2] = {k0, k0};
    int v2[2] = {0, 0};
    int v3[2] = {1, 0};
    int rank1, rank2;
    MPI_Cart_create(MPI_COMM_WORLD, 2, v1, v2, 0, &MPI_COMM_GRID);
    MPI_Cart_sub(MPI_COMM_GRID, v3, &MPI_COMM_COL);
    MPI_Comm_rank(MPI_COMM_GRID, &rank1);
    MPI_Comm_rank(MPI_COMM_COL, &rank2);
    int coords[2];
    int num;
    MPI_Cart_coords(MPI_COMM_GRID, rank1, 2, coords);
    pt << coords[0] << coords[1] << rank2;
    if (rank2 == 0)
    {
        num = k0 - 1;
    }
    else
    {
        num = rank2 - 1;
    }
    int source = (rank2 + 1) % k0;
    MPI_Sendrecv_replace(b_, p0 * q0, MPI_INT, num, 0, source, 0, MPI_COMM_COL, MPI_STATUSES_IGNORE);
    for (int i = 0; i < p0 * q0; i++)
    {
        pt << b_[i];
    }
}
