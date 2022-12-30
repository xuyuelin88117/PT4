#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI8Inter2");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	MPI_Comm comm1;
	MPI_Comm_dup(MPI_COMM_WORLD, &comm1);
	int n1;
	pt >> n1;
	MPI_Comm comm2;
	if (!n1)
		MPI_Comm_split(MPI_COMM_WORLD, n1, rank, &comm2);
	else
		MPI_Comm_split(MPI_COMM_WORLD, n1, size - rank - 1, &comm2);
	int n2;
	MPI_Comm_rank(comm2, &n2);
	pt << n2;
	MPI_Comm comm3;
	int n3 = size - 1;
	if (rank == size - 1)
		n3 = 0;
	MPI_Intercomm_create(comm2, 0, comm1, n3, 100, &comm3);
	double d1;
	pt >> d1;
	MPI_Send(&d1, 1, MPI_DOUBLE, n2, 0, comm3);
	double d2;
	MPI_Status status;
	MPI_Recv(&d2, 1, MPI_DOUBLE, n2, 0, comm3, &status);
	pt << d2;
}