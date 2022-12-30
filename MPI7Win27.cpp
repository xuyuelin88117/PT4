#include "pt4.h"
#include "mpi.h"
void Solve()
{
    Task("MPI7Win27");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    double v[2] = {0};
    MPI_Win window;
    if (rank)
    {
        pt >> v[0] >> v[1];
    }
    MPI_Win_create(v, 2 * 8, 8, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
    double res[2];
    if (rank == 0)
    {
        double V[2] = {v[0], v[1]};
        for (int i = 1; i < size; ++i)
        {

            MPI_Win_lock(MPI_LOCK_SHARED, i, 0, window);
            MPI_Get(V, 2, MPI_DOUBLE, i, 0, 2, MPI_DOUBLE, window);
            MPI_Win_unlock(i, window);
            ShowLine(V[0]);
            ShowLine(V[1]);
            if ((V[0] * V[0] + V[1] * V[1]) >= (v[0] * v[0] + v[1] * v[1]))
            {
                v[0] = V[0];
                v[1] = V[1];
            }
        }
        MPI_Barrier(MPI_COMM_WORLD);
    }
    else
    {

        MPI_Barrier(MPI_COMM_WORLD);
        MPI_Win_lock(MPI_LOCK_SHARED, 0, 0, window);
        MPI_Get(res, 2, MPI_DOUBLE, 0, 0, 2, MPI_DOUBLE, window);
        MPI_Win_unlock(0, window);

        pt << res[0] << res[1];
    }
    MPI_Win_free(&window);
}
