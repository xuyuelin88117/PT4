#include "mpi.h"
#include "pt4.h"

void Solve()
{

	Task("MPI8Inter3");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	float n1;
	MPI_Comm comm1;
	MPI_Comm_dup(MPI_COMM_WORLD, &comm1);
	int rank3 = rank % 3, res, sirank = size - rank, add_rank_1, add_rank_2;
	MPI_Comm comm2;
	if (rank3 == 2)
	{
		pt >> n1;
		MPI_Comm_split(MPI_COMM_WORLD, 2, rank, &comm2);
		MPI_Comm_rank(comm2, &res);
	}
	float n2;
	if (rank3 == 1)
	{
		pt >> n2 >> n1;
		MPI_Comm_split(MPI_COMM_WORLD, 1, sirank, &comm2);
		MPI_Comm_rank(comm2, &res);
	}
	if (rank3 == 0)
	{
		pt >> n2;
		MPI_Comm_split(MPI_COMM_WORLD, 0, rank, &comm2);
		MPI_Comm_rank(comm2, &res);
	}

	MPI_Comm comm3;
	if (rank3 == 2)
	{
		MPI_Intercomm_create(comm2, 0, MPI_COMM_WORLD, size - 2, 2, &comm3);
		MPI_Comm_rank(comm3, &add_rank_2);
	}
	MPI_Comm comm4;
	if (rank3 == 1)
	{
		MPI_Intercomm_create(comm2, 0, comm1, 0, 1, &comm4);
		MPI_Comm_rank(comm4, &add_rank_1);
		MPI_Intercomm_create(comm2, 0, MPI_COMM_WORLD, 2, 2, &comm3);
		MPI_Comm_rank(comm3, &add_rank_2);
	}

	if (rank3 == 0)
	{
		MPI_Intercomm_create(comm2, 0, comm1, size - 2, 1, &comm4);
		MPI_Comm_rank(comm4, &add_rank_1);
	}
	pt << res;
	float n3;
	if (rank3 == 2)
	{
		MPI_Send(&n1, 1, MPI_FLOAT, sirank, 0, comm1);
		MPI_Recv(&n3, 1, MPI_FLOAT, MPI_ANY_SOURCE, 0, comm1, MPI_STATUS_IGNORE);
		pt << n3;
	}
	float n4;
	if (rank3 - 1 == 0)
	{
		MPI_Recv(&n4, 1, MPI_FLOAT, MPI_ANY_SOURCE, 0, comm1, MPI_STATUS_IGNORE);
		MPI_Recv(&n3, 1, MPI_FLOAT, MPI_ANY_SOURCE, 0, comm1, MPI_STATUS_IGNORE);
		MPI_Send(&n2, 1, MPI_FLOAT, sirank - 2, 0, comm1);
		MPI_Send(&n1, 1, MPI_FLOAT, sirank, 0, comm1);
		pt << n4 << n3;
	}

	if (rank3 == 0)
	{
		MPI_Send(&n2, 1, MPI_FLOAT, sirank - 2, 0, comm1);
		MPI_Recv(&n4, 1, MPI_FLOAT, MPI_ANY_SOURCE, 0, comm1, MPI_STATUS_IGNORE);
		pt << n4;
	}
}