#include "pt4.h"
#include "mpi.h"
#include <vector>

using namespace std;

void Solve()
{

    Task("MPI6File10");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int num = rank;
    char file[30];
    MPI_File mpifile;
    if (rank == 0)
        pt >> file;
    MPI_Bcast(file, 30, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_CREATE | MPI_MODE_RDWR, MPI_INFO_NULL, &mpifile);
    vector<int> v(num + 1);
    for (int i = 0; i < num; ++i)
        pt >> v[i];
    MPI_File_seek(mpifile, (num * (num - 1)) / 2 * sizeof(int), MPI_SEEK_SET);
    MPI_File_write_all(mpifile, &v[0], num, MPI_INT, MPI_STATUS_IGNORE);
    MPI_File_close(&mpifile);
}