#include "pt4.h"
#include "mpi.h"
void Solve()
{
	Task("MPI6File28");
	int flag;
	MPI_Initialized(&flag);
	if (flag == 0)
		return;
	int rank, size;
	MPI_Comm_size(MPI_COMM_WORLD, &size);
	MPI_Comm_rank(MPI_COMM_WORLD, &rank);

	char file[20];
	MPI_File mpifile;
	MPI_Datatype datatype;
	if (rank == 0)
		pt >> file;
	MPI_Bcast(file, 20, MPI_CHAR, 0, MPI_COMM_WORLD);
	int n;
	pt >> n;
	double *v = new double[size / 2];
	for (int i = 0; i < size / 2; i++)
		pt >> v[i];
	MPI_File_open(MPI_COMM_WORLD, file, MPI_MODE_CREATE | MPI_MODE_WRONLY, MPI_INFO_NULL, &mpifile);
	int s;
	MPI_Type_size(MPI_DOUBLE, &s);
	MPI_Type_vector(size / 2, 1, size, MPI_DOUBLE, &datatype);
	MPI_File_set_view(mpifile, (n - 1) * s, MPI_DOUBLE, datatype, "native", MPI_INFO_NULL);
	MPI_File_write_all(mpifile, v, size / 2, MPI_DOUBLE, MPI_STATUS_IGNORE);
	MPI_File_close(&mpifile);
}
