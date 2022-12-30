#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI7Win30");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	int *v1 = new int[size + 2];
	MPI_Win window;
	int *v2 = new int[size + 2];
	int n = 0;
	for (int i = 0; i < size; i++)
	{
		pt >> v1[i];
		n += v1[i];
	}
	v1[size] = n;
	v1[size + 1] = 1;
	MPI_Group g1, g2;
	MPI_Comm_group(MPI_COMM_WORLD, &g1);
	if (rank == 0)
	{

		MPI_Win_create(v1, (size + 2) * 4, 4, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
		for (int i = 1; i < size; i++)
		{
			int k[1] = {i};
			MPI_Group_incl(g1, 1, k, &g2);
			MPI_Win_post(g2, 0, window);
			MPI_Win_wait(window);
			MPI_Win_post(g2, 0, window);
			MPI_Win_wait(window);
		}
		MPI_Barrier(MPI_COMM_WORLD);
		for (int i = 0; i < size + 2; i++)
			pt << v1[i];
	}
	else
	{
		MPI_Win_create(nullptr, (size + 2) * 4, 4, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
		int *tmp_row = new int[size + 2];
		int k[1] = {0};
		MPI_Group_incl(g1, 1, k, &g2);
		MPI_Win_start(g2, 0, window);
		MPI_Get(tmp_row, size + 2, MPI_INT, 0, 0, size + 2, MPI_INT, window);
		MPI_Win_complete(window);
		if (v1[size] <= tmp_row[size])
		{
			if (v1[size] == tmp_row[size])
				v1[size + 1] += tmp_row[size + 1];
			for (int j = 0; j < size + 2; j++)
				tmp_row[j] = v1[j];
		}
		MPI_Win_start(g2, 0, window);
		MPI_Put(tmp_row, size + 2, MPI_INT, 0, 0, size + 2, MPI_INT, window);
		MPI_Win_complete(window);
		MPI_Barrier(MPI_COMM_WORLD);
		MPI_Win_lock(MPI_LOCK_SHARED, 0, 0, window);
		MPI_Get(v2, size + 2, MPI_INT, 0, 0, size + 2, MPI_INT, window);
		MPI_Win_unlock(0, window);
		for (int i = 0; i < size + 2; i++)
			pt << v2[i];
	}
}
