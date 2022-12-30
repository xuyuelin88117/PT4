#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI6File6");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    char file[12];
    MPI_File mpifile;
    if (rank == 0)
        pt >> file;
    MPI_Bcast(file, 12, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &mpifile);
    MPI_Offset offset = (size - 1) * (size - rank - 1) * sizeof(int);
    if (rank > 0)
    {
        int n;
        pt >> n;
        MPI_File_seek(mpifile, offset, MPI_SEEK_SET);
        for (int i = 0; i < size - 1; i++)
            MPI_File_write(mpifile, &n, 1, MPI_INT, MPI_STATUS_IGNORE);
    }
    MPI_File_close(&mpifile);
}
