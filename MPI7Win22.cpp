#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI7Win22");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	double *res = new double[rank];
	MPI_Win window;
	double *v = new double[rank];
	int n[8];
	if (!rank)
	{
		v = new double[size - 1];
		for (int i = 0; i < size - 1; i++)
			pt >> v[i];
		for (int i = 0; i < 8; i++)
			pt >> n[i];
	}
	else
		for (int i = 0; i < rank; i++)
			pt >> res[i];

	int Type_size;
	MPI_Type_size(MPI_DOUBLE, &Type_size);
	MPI_Win_create(res, rank * Type_size, Type_size, MPI_INFO_NULL, MPI_COMM_WORLD, &window);
	MPI_Group g1;
	MPI_Comm_group(MPI_COMM_WORLD, &g1);
	int r = 0;
	MPI_Group g2;
	if (!rank)
	{
		MPI_Group_excl(g1, 1, &r, &g2);
		MPI_Win_start(g2, 0, window);
		for (int i = 0; i < 8; i++)
		{
			MPI_Accumulate(v, n[i], MPI_DOUBLE, n[i], 0, n[i], MPI_DOUBLE, MPI_SUM, window);
		}
		MPI_Win_complete(window);
	}
	else
	{
		MPI_Group_incl(g1, 1, &r, &g2);
		MPI_Win_post(g2, 0, window);
		MPI_Win_wait(window);
	}
	for (int i = 0; i < rank; i++)
		pt << res[i];
}
