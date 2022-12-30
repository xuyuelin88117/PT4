#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI4Type13");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    char packbuf[100];
    if (rank != 0) {
        int* a = new int[rank];
        double b;
        pt >> b;
        for (int i = 0; i < rank; i++)
            pt >> a[i];
        int packsize = 0;
        MPI_Pack(&b, 1, MPI_DOUBLE, packbuf, 100, &packsize, MPI_COMM_WORLD);
        for (int i = 0; i < rank; i++)
            MPI_Pack(&a[i], 1, MPI_INT, packbuf, 100, &packsize, MPI_COMM_WORLD);
        MPI_Send(&packsize, 1, MPI_INT, 0, 1, MPI_COMM_WORLD);
        MPI_Send(&packbuf, packsize, MPI_PACKED, 0, 0, MPI_COMM_WORLD);
        delete[] a;
    }
    else {
        for (int i = 1; i < size; ++i) {
            double b;
            int* a = new int[i];
            int packsize;
            MPI_Recv(&packsize, 1, MPI_INT, i, 1, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            MPI_Recv(&packbuf, packsize, MPI_PACKED, i, 0, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            int position = 0;
            MPI_Unpack(packbuf, packsize, &position, &b, 1, MPI_DOUBLE, MPI_COMM_WORLD);
            pt << b;
            for (int j = 0; j < i; j++) {
                MPI_Unpack(packbuf, packsize, &position, &a[j], 1, MPI_INT, MPI_COMM_WORLD);
                pt << a[j];
            }
            delete[] a;
        }
    }

}
