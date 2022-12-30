#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI1Proc10");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    int n;
	pt >> n;
	if ( rank == 0 ) {
		if ( size % 2 == 0 ) {
			float f;
			for ( int i = 0; i <  n; i ++) {
				float nom;
				pt >> nom;
				if ( i == 0) {
					f = nom;
					continue;
				} else if ( f > nom )
					f = nom;
			}
			pt << f;
		} else {
			int f;
			for ( int i = 0; i <  n; i ++) {
				int nom;
				pt >> nom;
				if ( i == 0) {
					f = nom;
					continue;
				} else if ( f > nom )
					f = nom;
			}
			pt << f;
		}
	} else if ( rank % 2 == 0 ) {
		int f;
		for ( int i = 0; i <  n; i ++) {
			int nom;
			pt >> nom;
			if ( i == 0) {
				f = nom;
				continue;
			} else if ( f > nom )
				f = nom;
		}
		pt << f;
	} else {
		float f;
		for ( int i = 0; i <  n; i ++) {
			float nom;
			pt >> nom;
			if ( i == 0) {
				f = nom;
				continue;
			} else if ( f < nom )
				f = nom;
		}
		pt << f;
	}
}
