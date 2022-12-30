#include "mpi.h"
#include "pt4.h"

void Solve()
{

	Task("MPI7Win19");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	float *v = new float[size];
	MPI_Win window;
	int n = size - rank - 1;
	if (rank == 0)
		for (int i = 0; i < size - 1; i++)
			pt >> v[i];
	MPI_Win_create(v, (size - 1) * sizeof(float), sizeof(float), MPI_INFO_NULL, MPI_COMM_WORLD, &window);
	MPI_Win_fence(0, window);
	float d = 0;
	if (rank != 0)
		MPI_Get(&d, 1, MPI_FLOAT, 0, n, 1, MPI_FLOAT, window);
	MPI_Win_fence(0, window);
	if (rank != 0)
		pt << d;
}