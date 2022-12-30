#include "mpi.h"
#include "pt4.h"

void Solve()
{
    Task("MPI6File21");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    char file[size];
    MPI_File mpifile;
    MPI_Datatype mpidatatype;
    const int s = 20, num = 3;
    int v[num];
    if (rank == 0)
        pt >> file;
    MPI_Bcast(file, s, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_CREATE | MPI_MODE_RDWR, MPI_INFO_NULL, &mpifile);
    MPI_Type_vector(num, 1, size, MPI_INT, &mpidatatype);
    MPI_File_set_view(mpifile, sizeof(int) * rank, MPI_INT, mpidatatype, (char *)"native", MPI_INFO_NULL);
    MPI_File_read_all(mpifile, v, num, MPI_INT, MPI_STATUS_IGNORE);
    for (int i = 0; i < num; i++)
        pt << v[i];
    MPI_File_close(&mpifile);
}