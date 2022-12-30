#include "pt4.h"
#include "mpi.h"

#include <cmath>
using namespace std;
int k; // number of processes
int r; // rank of the current process

int m, p, q; // sizes of the given matrices
int na, nb;  // sizes of the matrix bands

int *a_, *b_, *c_; // arrays to store matrices in the master process
int *a, *b, *c;    // arrays to store matrix bands in each process

MPI_Datatype MPI_COLS; // datatype for the band of the matrix B

void Solve()
{
    Task("MPI9Matr12");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    k = size;
    r = rank;

    int *num = new int[3];
    if (r == 0)
    {
        pt >> m >> p >> q;
        a_ = new int[m * p];
        b_ = new int[p * q];
        for (int i = 0; i < m * p; i++)
        {
            pt >> a_[i];
        }
        for (int i = 0; i < p * q; i++)
        {
            pt >> b_[i];
        }
        na = ceil((double)m / k);
        nb = ceil((double)q / k);
        num[0] = na;
        num[1] = nb;
        num[2] = p;
    }
    MPI_Bcast(num, 3, MPI_INT, 0, MPI_COMM_WORLD);
    na = num[0];
    nb = num[1];
    p = num[2];
    a = new int[na * p];
    b = new int[p * nb];
    c = new int[na * k * nb];
    fill(c, c + na * k * nb, 0);
    int *m1 = new int[na * p * k];
    fill(m1, m1 + na * p * k, 0);
    int *m2 = new int[p * nb * k];
    fill(m2, m2 + p * nb * k, 0);
    MPI_Type_vector(p, nb, nb * k, MPI_INT, &MPI_COLS);
    MPI_Type_commit(&MPI_COLS);
    if (r == 0)
    {
        for (int i = 0; i < na * p * k; i++)
        {
            if (i < m * p)
            {
                m1[i] = a_[i];
            }
        }
        int t = 0;
        for (int i = 0; i < p * nb * k; i++)
        {
            if (i % (nb * k) < q)
            {
                m2[i] = b_[t++];
            }
        }
    }
    MPI_Scatter(m1, na * p, MPI_INT, a, na * p, MPI_INT, 0, MPI_COMM_WORLD);
    if (r == 0)
    {
        MPI_Sendrecv(m2, 1, MPI_COLS, 0, 0, b, p * nb, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUSES_IGNORE);
        for (int i = 1; i < k; i++)
        {
            MPI_Send(m2 + nb * i, 1, MPI_COLS, i, 0, MPI_COMM_WORLD);
        }
    }
    else
    {
        MPI_Recv(b, p * nb, MPI_INT, 0, 0, MPI_COMM_WORLD, MPI_STATUSES_IGNORE);
    }
    pt << na << p << nb;
    for (int i = 0; i < na * p; i++)
    {
        pt << a[i];
    }
    for (int i = 0; i < nb * p; i++)
    {
        pt << b[i];
    }
    for (int i = 0; i < na * k * nb; i++)
    {
        pt << c[i];
    }
}
