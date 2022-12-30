#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI6File30");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    char file[12];
    int n = 0;
    MPI_File mpifile;
    MPI_Datatype datatype;
    char native[7] = "native";

    if (rank == 0)
        pt >> file >> n;
    MPI_Bcast(file, 12, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &mpifile);
    int n1 = 0, n2 = 0;
    pt >> n1 >> n2;
    MPI_Type_vector(n, n, 3 * n, MPI_INT, &datatype);
    MPI_File_set_view(mpifile, ((n2 - 1) * n + (n1 - 1) * 3 * n * n) * sizeof(int), MPI_INT, datatype, native, MPI_INFO_NULL);
    n = n * n;
    int *v = new int[n];
    for (int i = 0; i < n; i++)
        v[i] = rank;
    MPI_File_write_all(mpifile, v, n, MPI_INT, MPI_STATUS_IGNORE);
    MPI_File_close(&mpifile);
}