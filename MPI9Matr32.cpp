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
    Task("MPI9Matr32");
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

    pt >> m >> p;
    m0 = m / k0;
    p0 = p / k0;
    // int matsize = m0 * p0;
    a = new int[m0 * p0];
    if (rank == 0)
    {
        int tmp = m * p;
        a_ = new int[tmp];
        for (int i = 0; i < tmp; i++)
            pt >> a_[i];
        MPI_Type_vector(m0, p0, p, MPI_INT, &MPI_BLOCK_A);
        MPI_Type_commit(&MPI_BLOCK_A);
        tmp = 0;
        for (int i = 1; i < k; i++)
        {
            MPI_Send(&a_[tmp + p0 * (i % k0)], 1, MPI_BLOCK_A, i, 0, MPI_COMM_WORLD);
            if ((i + 1) % k0 == 0)
                tmp += p * m0;
        }
        MPI_Sendrecv(a_, 1, MPI_BLOCK_A, 0, 0, a, m0 * p0, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        for (int i = 0; i < m0 * p0; i++)
            pt << a[i];
    }
    else
    {
        MPI_Recv(a, m0 * p0, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        for (int i = 0; i < m0 * p0; i++)
            pt << a[i];
    }
}
