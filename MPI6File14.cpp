#include "pt4.h"
#include "mpi.h"
#include <vector>
using namespace std;

void Solve()
{
    Task("MPI6File14");
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
    MPI_Comm comm;
    int num = 0;
    int n = 0;
    pt >> n;
    if (n != 0)
        num = 1;
    if (num != 0)
        MPI_Comm_split(MPI_COMM_WORLD, num, rank, &comm);
    else
        MPI_Comm_split(MPI_COMM_WORLD, MPI_UNDEFINED, rank, &comm);
    if (comm == MPI_COMM_NULL)
        return;
    MPI_File_open(comm, file, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &mpifile);
    MPI_Comm_size(comm, &size);
    vector<int> v;
    for (int i = 0; i < size; i++)
        v.push_back(n);
    MPI_File_write_ordered(mpifile, &v[0], size, MPI_INT, MPI_STATUSES_IGNORE);
    MPI_File_close(&mpifile);
}