#include "pt4.h"
#include "mpi.h"

#include <cmath>

int k; // number of processes
int r; // rank of the current process

int m, p, q; // sizes of the given matrices
int na, nb;  // sizes of the matrix bands

int *a_, *b_, *c_; // arrays to store matrices in the master process
int *a, *b, *c;    // arrays to store matrix bands in each process

void Solve()
{
    Task("MPI9Matr4");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    k = size;
    r = rank;

    pt >> na >> p >> nb >> q;
    a = new int[na * p];
    b = new int[nb * q];
    c = new int[na * q];
    for (int i = 0; i < na * p; i++)
    {
        pt >> a[i];
    }
    for (int i = 0; i < nb * q; i++)
    {
        pt >> b[i];
    }
    for (int i = 0; i < na * q; i++)
    {
        pt >> c[i];
    }
    for (int i = 0; i < na; i++)
    {
        for (int j = 0; j < q; j++)
        {
            int res = 0;
            int nn = (0 + r) * nb;
            if (nn >= nb * k)
            {
                nn -= nb * k;
                nn = (nn / nb) * nb;
            }
            for (int ii = 0; ii < nb; ii++)
            {
                if (nn + ii >= p)
                {
                    break;
                }
                res += a[p * i + nn + ii] * b[q * ii + j];
            }
            c[q * i + j] += res;
        }
    }
    MPI_Status status;
    int panduan;
    if (r == 0)
    {
        panduan = k - 1;
    }
    else
    {
        panduan = (r - 1) % k;
    }
    MPI_Sendrecv_replace(b, nb * q, MPI_INT, panduan, 0, (r + 1) % k, 0, MPI_COMM_WORLD, &status);
    for (int i = 0; i < na; i++)
    {
        for (int j = 0; j < q; j++)
        {
            int res = 0;
            int nn = (1 + r) * nb;
            if (nn >= nb * k)
            {
                nn -= nb * k;
                nn = (nn / nb) * nb;
            }
            for (int ii = 0; ii < nb; ii++)
            {
                if (nn + ii >= p)
                {
                    break;
                }
                res += a[p * i + nn + ii] * b[q * ii + j];
            }
            c[q * i + j] += res;
        }
    }
    if (r == 0)
    {
        panduan = k - 1;
    }
    else
    {
        panduan = (r - 1) % k;
    }
    MPI_Sendrecv_replace(b, nb * q, MPI_INT, panduan, 0, (r + 1) % k, 0, MPI_COMM_WORLD, &status);
    for (int i = 0; i < na * q; i++)
    {
        pt << c[i];
    }
    for (int i = 0; i < nb * q; i++)
    {
        pt << b[i];
    }
}
