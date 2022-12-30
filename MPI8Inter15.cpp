#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI8Inter15");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    MPI_Comm comm1 = MPI_COMM_NULL;
    MPI_Comm_get_parent(&comm1);
    char v[10];
    strcpy_s(v, "ptprj.exe");
    int n;
    double d1;
    if (comm1 == MPI_COMM_NULL)
    {
        MPI_Comm_spawn(v, NULL, 1, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm1, MPI_ERRCODES_IGNORE);
        pt >> d1;
        n = 0;
    }
    else
        n = MPI_ROOT;
    double d2;
    MPI_Reduce(&d1, &d2, 1, MPI_DOUBLE, MPI_SUM, n, comm1);
    MPI_Bcast(&d2, 1, MPI_DOUBLE, n, comm1);
    if (n != MPI_ROOT)
        pt << d2;
}
