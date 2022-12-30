#include "pt4.h"
#include "mpi.h"

#include <cmath>

int k; // number of processes
int r; // rank of the current process

int m, p, q;    // sizes of the given matrices
int m0, p0, q0; // sizes of the matrix blocks
int k0;         // order of the Cartesian grid (equal to sqrt(k))

int *a_, *b_, *c_; // arrays to store matrices in the master process
int *a, *b, *c;    // arrays to store matrix blocks in each process

MPI_Datatype MPI_BLOCK_A; // datatype for the block of the matrix A
MPI_Datatype MPI_BLOCK_B; // datatype for the block of the matrix B
MPI_Datatype MPI_BLOCK_C; // datatype for the block of the matrix C

MPI_Comm MPI_COMM_GRID = MPI_COMM_NULL;
// communicator associated with a two-dimensional Cartesian grid

void Solve()
{
    Task("MPI9Matr26");
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

    pt >> m0 >> p0 >> q0;
    a = new int[m0 * p0];
    b = new int[p0 * q0];
    c = new int[m0 * q0];
    for (int i = 0; i < m0 * p0; i++)
    {
        pt >> a[i];
    }
    for (int i = 0; i < p0 * q0; i++)
    {
        pt >> b[i];
    }
    for (int i = 0; i < m0 * q0; i++)
    {
        pt >> c[i];
    }
    int input;
    pt >> input;
    int v1[2] = {k0, k0};
    int v2[2] = {true, true};
    MPI_Cart_create(MPI_COMM_WORLD, 2, v1, v2, false, &MPI_COMM_GRID);
    int shift1, shift2, shift3, shift4;//
    MPI_Cart_shift(MPI_COMM_GRID, 1, -1, &shift1, &shift2);
    MPI_Cart_shift(MPI_COMM_GRID, 0, -1, &shift3, &shift4);
    for (int i = 0; i < input; i++)
    {
        for (int ii = 0; ii < m0; ii++)
        {
            for (int j = 0; j < q0; j++)
            {
                for (int k = 0; k < p0; k++)
                {
                    c[ii * q0 + j] += a[ii * p0 + k] * b[k * q0 + j];
                }
            }
        }
        MPI_Sendrecv_replace(a, m0 * p0, MPI_INT, shift2, 0, shift1, 0, MPI_COMM_GRID, MPI_STATUS_IGNORE);
        MPI_Sendrecv_replace(b, p0 * q0, MPI_INT, shift4, 0, shift3, 0, MPI_COMM_GRID, MPI_STATUS_IGNORE);
    }
    for (int i = 0; i < m0 * q0; i++)
    {
        pt << c[i];
    }
    for (int i = 0; i < m0 * p0; i++)
    {
        pt << a[i];
    }
    for (int i = 0; i < p0 * q0; i++)
    {
        pt << b[i];
    }
}
