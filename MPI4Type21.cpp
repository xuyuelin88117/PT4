#include "pt4.h"
#include "mpi.h"
using namespace std;
void Solve()
{
	Task("MPI4Type21");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	int n = size - 1;
	MPI_Datatype type;
	MPI_Type_vector(n, 1, n, MPI_DOUBLE, &type);
	MPI_Type_commit(&type);
	double *douN = new double[n * n];

	if (rank == 0)
	{
		for (int i = 0; i < n * n; ++i)
			pt >> douN[i];
	}
	int *count = new int[size];
	int *sd = new int[size];

	MPI_Datatype *ty = new MPI_Datatype[size];
	int *recount = new int[size];
	int *rd = new int[size];
	MPI_Datatype *retype = new MPI_Datatype[size];

	if (rank == 0)
	{
		count[0] = 0;
		sd[0] = 0;

		fill(ty, ty + size, type);
		for (int i = 1; i < size; i++)
		{
			sd[i] = (i - 1) * sizeof(double);

			count[i] = 1;
		}
		fill(rd, rd + size, 0);
		fill(recount, recount + size, 0);
		fill(retype, retype + size, type);
	}
	else
	{
		count[0] = 0;
		sd[0] = 0;

		fill(ty, ty + size, type);
		fill(count, count + size, 0);
		fill(sd, sd + size, 0);

		fill(rd, rd + size, 0);
		fill(recount, recount + size, 0);
		recount[0] = n;
		fill(retype, retype + size, MPI_DOUBLE);
	}
	double *res = new double[n];
	MPI_Alltoallw(douN, count, sd, ty, res, recount, rd, retype, MPI_COMM_WORLD);

	if (rank != 0)
		for (int i = 0; i < n; ++i)
			pt << res[i];
}
