#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI8Inter19");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	MPI_Comm comm1;
	MPI_Comm_get_parent(&comm1);
	int *v = new int[2 * size];
	int r;
	int n1;
	if (comm1 == MPI_COMM_NULL)
	{
		MPI_Comm_spawn("ptprj.exe", NULL, size, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm1, MPI_ERRCODES_IGNORE);
		if (rank == 0)
		{
			for (int i = 0; i < 2 * size; i++)
				pt >> v[i];
		}
		r = 0;
		n1 = 0;
	}
	else
	{
		r = MPI_ROOT;
		n1 = 1;
	}
	MPI_Comm comm2;
	MPI_Intercomm_merge(comm1, n1, &comm2);
	int rank2;
	MPI_Comm_rank(comm2, &rank2);
	int vn;
	MPI_Scatter(v, 1, MPI_INT, &vn, 1, MPI_INT, 0, comm2);
	if (rank2 < size)
		pt << vn;
	int n2;
	MPI_Reduce(&vn, &n2, 1, MPI_INT, MPI_SUM, 1, comm2);
	if (rank == 1)
		pt << n2;
}
