#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI7Win17");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	double *res = new double[size];
	MPI_Win window;
	double *v = new double[size];
	for (int i = 0; i < size; i++)
		pt >> res[i];
	pt >> v[0];
	for (int i = 0; i < size; i++)
		v[i] = v[0];
	
	MPI_Win_create(res, 8 * size, 8, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
	MPI_Win_fence(0, window);
	MPI_Accumulate(v, size, MPI_DOUBLE, (rank - 1 + size) % size, 0, size, MPI_DOUBLE, MPI_MIN, window);
	MPI_Win_fence(0, window);
	for (int i = 0; i < size; i++)
	{
		if (rank == 0)
			continue;
		MPI_Accumulate(res, 1, MPI_DOUBLE, i, rank, 1, MPI_DOUBLE, MPI_SUM, window);
	}
	MPI_Win_fence(0, window);
	for (int i = 0; i < size; i++)
		pt << res[i];
}
