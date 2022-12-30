#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI8Inter16");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	MPI_Comm comm1;
	MPI_Comm_get_parent(&comm1);
	double *v = new double[size];
	int r;
	if (comm1 == MPI_COMM_NULL)
	{
		MPI_Comm_spawn("ptprj.exe", NULL, size, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm1, MPI_ERRCODES_IGNORE);
		for (int i = 0; i < size; i++)
			pt >> v[i];
		r = rank;
	}
	else
		r = MPI_ROOT;
	double dmax;
	MPI_Reduce_scatter_block(v, &dmax, 1, MPI_DOUBLE, MPI_MAX, comm1);
	if (r == MPI_ROOT)
		MPI_Send(&dmax, 1, MPI_DOUBLE, rank, 0, comm1);
	else
	{
		MPI_Recv(&dmax, 1, MPI_DOUBLE, rank, 0, comm1, MPI_STATUS_IGNORE);
		pt << dmax;
	}
}
