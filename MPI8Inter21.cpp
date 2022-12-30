#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI8Inter21");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	MPI_Comm comm1;

	MPI_Comm_get_parent(&comm1);

	double d1;
	MPI_Comm comm2;
	if (comm1 == MPI_COMM_NULL)
	{
		pt >> d1;
		MPI_Comm_spawn("ptprj.exe", NULL, 1, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm2, MPI_ERRCODES_IGNORE);
		if (rank == 0)
			MPI_Send(&d1, 1, MPI_DOUBLE, 0, 1995, comm2);
		MPI_Barrier(comm2);
		MPI_Comm_spawn("ptprj.exe", NULL, size - 1, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm1, MPI_ERRCODES_IGNORE); // !!!!
		if (rank > 0)
		{
			MPI_Send(&d1, 1, MPI_DOUBLE, rank - 1, 2019, comm1);
			MPI_Recv(&d1, 1, MPI_DOUBLE, rank - 1, 1337, comm1, MPI_STATUS_IGNORE);
			pt << d1;
		}
	}
	else
	{
		MPI_Status status;
		MPI_Recv(&d1, 1, MPI_DOUBLE, MPI_ANY_SOURCE, MPI_ANY_TAG, comm1, &status);
		char v[MPI_MAX_PORT_NAME];
		MPI_Comm comm3;
		if (status.MPI_TAG == 1995)
		{
			MPI_Open_port(MPI_INFO_NULL, v);
			MPI_Publish_name("nvidia", MPI_INFO_NULL, v);
			MPI_Barrier(comm1);
			MPI_Comm_accept(v, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm3);
			int remotesize;
			MPI_Comm_remote_size(comm3, &remotesize);
			for (int i = 0; i < remotesize; i++)
				MPI_Send(&d1, 1, MPI_DOUBLE, i, 359, comm3);
		}
		else
		{
			MPI_Lookup_name("nvidia", MPI_INFO_NULL, v);
			MPI_Comm_connect(v, MPI_INFO_NULL, 0, MPI_COMM_WORLD, &comm3);
			double d2;
			MPI_Recv(&d2, 1, MPI_DOUBLE, 0, 359, comm3, MPI_STATUS_IGNORE);
			d1 += d2;
			MPI_Send(&d1, 1, MPI_DOUBLE, rank + 1, 1337, comm1);
		}
	}
}