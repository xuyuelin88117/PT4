#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI6File23");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    char file[20];
    MPI_File mpifile;
    MPI_Datatype datatype;
    char native[7] = "native";
    if (rank == 0)
        pt >> file;
    MPI_Bcast(file, 20, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_RDWR | MPI_MODE_CREATE, MPI_INFO_NULL, &mpifile);
    MPI_Type_contiguous(3, MPI_DOUBLE, &datatype);
    MPI_Type_create_resized(datatype, 0, (3) * 8 * size, &datatype);
    MPI_File_set_view(mpifile, rank * 8 * 3, MPI_DOUBLE, datatype, native, MPI_INFO_NULL);
    double v[6];
    MPI_File_read_all(mpifile, v, 6, MPI_DOUBLE, MPI_STATUSES_IGNORE);
    for (int i = 0; i < 6; i++)
        pt << v[i];
}
