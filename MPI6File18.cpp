#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI6File18");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	char file[12];
	MPI_File mpifile;
	if (rank == 0)
		pt >> file;
	MPI_Bcast(file, 12, MPI_CHAR, 0, MPI_COMM_WORLD);
	int n;
	pt >> n;
	MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_RDWR, MPI_INFO_NULL, &mpifile);
	int s;
	MPI_Type_size(MPI_INT, &s);
	MPI_File_set_view(mpifile, 5 * rank * s, MPI_INT, MPI_INT, "native", MPI_INFO_NULL);
	MPI_File_write_at_all(mpifile, n - 1, &rank, 1, MPI_INT, MPI_STATUS_IGNORE);
	MPI_File_close(&mpifile);
}