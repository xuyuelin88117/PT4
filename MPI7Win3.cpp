#include "pt4.h"
#include "mpi.h"
#include <vector>

using namespace std;

void Solve()
{
    Task("MPI7Win3");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    const int N = size - 1;
    MPI_Win window;
    vector<int> v(N);
    if (rank == 0)
        for (int i = 0; i < N; ++i)
            pt >> v[i];
    MPI_Win_create(&v[0], N * sizeof(int), sizeof(int), MPI_INFO_NULL, MPI_COMM_WORLD, &window);
    MPI_Win_fence(0, window);
    int num;
    if (rank != 0)
        MPI_Get(&num, 1, MPI_INT, 0, N - rank, 1, MPI_INT, window);
    MPI_Win_fence(0, window);
    if (rank != 0)
        pt << num;
    MPI_Win_free(&window);
}