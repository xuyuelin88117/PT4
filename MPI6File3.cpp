#include "pt4.h"
#include "mpi.h"
#include <vector>

using namespace std;

void Solve()
{

    Task("MPI6File3");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int num = size - 1;
    char file[30];
    MPI_File mpifile;
    if (rank == 0)
        pt >> file;
    MPI_Bcast(file, 30, MPI_CHAR, 0, MPI_COMM_WORLD);
    MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_RDONLY, MPI_INFO_NULL, &mpifile);
    MPI_Offset fsize;
    MPI_File_get_size(mpifile, &fsize);
    if (rank > 0)
    {
        int n = fsize / sizeof(double) / num;
        vector<double> result(n);
        MPI_File_read_at(mpifile, (rank - 1) * n * sizeof(double), &result[0], n, MPI_DOUBLE, MPI_STATUS_IGNORE);
        for (auto tmp : result)
            pt << tmp;
    }
}