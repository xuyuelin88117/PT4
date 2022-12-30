#include "pt4.h"
#include "mpi.h"

#include <cmath>

int k; // number of processes
int r; // rank of the current process

int m, p, q; // sizes of the given matrices
int na, nb;  // sizes of the matrix bands

int *a_, *b_, *c_; // arrays to store matrices in the master process
int *a, *b, *c;    // arrays to store matrix bands in each process

MPI_Datatype MPI_COLS; // datatype for the band of the matrix B

void Solve()
{
    Task("MPI9Matr16");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    k = size;
    r = rank;

    if (r == 0)
    {
        pt >> m >> q;
    }
    pt >> na >> nb;
    c = new int[na * k * nb];
    if (r == 0)
    {
        c_ = new int[na * k * nb * k];
    }
    for (int i = 0; i < na * k * nb; i++)
    {
        pt >> c[i];
    }
    MPI_Type_vector(na * k, nb, nb * k, MPI_INT, &MPI_COLS);
    MPI_Type_commit(&MPI_COLS);
    if (r != 0)
    {
        MPI_Send(c, na * nb * k, MPI_INT, 0, 0, MPI_COMM_WORLD);
    }
    else
    {
        MPI_Status status;
        MPI_Sendrecv(c, na * nb * k, MPI_INT, 0, 0, c_, 1, MPI_COLS, 0, 0, MPI_COMM_WORLD, &status);
        for (int i = 1; i < k; i++)
        {
            MPI_Recv(&c_[i * nb], 1, MPI_COLS, i, 0, MPI_COMM_WORLD, &status);
        }
    }
    if (r == 0)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < q; j++)
            {
                pt << c_[i * (nb * k) + j];
            }
        }
    }
}
