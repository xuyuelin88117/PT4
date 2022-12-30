#include "pt4.h"
#include "mpi.h"
#include <vector>

using std::vector;

void Solve()
{
	Task("MPI8Inter18");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	MPI_Comm comm1;
	MPI_Comm_get_parent(&comm1);
	vector<int> v(1);
	int n = 0;
	if (comm1 == MPI_COMM_NULL)
	{
		v.assign(ptin_iterator<int>(size / 2), ptin_iterator<int>());
		MPI_Comm_spawn("ptprj.exe", NULL, size, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm1, MPI_ERRCODES_IGNORE);
		if (rank == 0 || rank == 1)
			n = MPI_ROOT;
		else
			n = MPI_PROC_NULL;
	}
	MPI_Comm comm2;
	MPI_Comm_split(comm1, (rank % 2), rank, &comm2);
	MPI_Op op;
	if (rank % 2 == 0)
		op = MPI_MIN;
	else
		op = MPI_MAX;
	int num;
	MPI_Reduce_scatter_block(&v[0], &num, 1, MPI_INT, op, comm2);
	MPI_Reduce(&num, &num, 1, MPI_INT, op, n, comm2);
	if (n != 0 && n == MPI_ROOT)
		pt << num;
}