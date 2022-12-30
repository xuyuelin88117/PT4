#include "pt4.h"
#include "mpi.h"

#include <cmath>

#define gravity 10 // gravitational constant
#define dt 0.1     // time step
#define N 800      // number of particles
#define fmax 1     // max force value

int k;     // number of processes
int r;     // rank of the current process
int Niter; // number of iterations

struct Particle
{
    double x, y, vx, vy;
};

struct Force
{
    double x, y;
};

Particle p[N];
Force f[N];
double m[N];

void Init()
{
    for (int i = 0; i < N; i++)
    {
        p[i].x = 20 * (i / 20 - 20) + 10;
        p[i].y = 20 * (i % 20 - 10) + 10;
        p[i].vx = p[i].y / 15;
        p[i].vy = -p[i].x / 50;

        m[i] = 100 + i % 100;

        f[i].x = 0;
        f[i].y = 0;
    }
}

// Implementation of a non-parallel algorithm

void CalcForces2()
{
    for (int i = 0; i < N - 1; i++)
        for (int j = i + 1; j < N; j++)
        {
            double dx = p[j].x - p[i].x, dy = p[j].y - p[i].y,
                   r_2 = 1 / (dx * dx + dy * dy),
                   r_1 = sqrt(r_2),
                   fabs = gravity * m[i] * m[j] * r_2;
            if (fabs > fmax)
                fabs = fmax;
            f[i].x += dx = fabs * dx * r_1;
            f[i].y += dy = fabs * dy * r_1;
            f[j].x -= dx;
            f[j].y -= dy;
        }
}

void MoveParticlesAndFreeForces()
{
    for (int i = 0; i < N; i++)
    {
        double dvx = f[i].x * dt / m[i],
               dvy = f[i].y * dt / m[i];
        p[i].x += (p[i].vx + dvx / 2) * dt;
        p[i].y += (p[i].vy + dvy / 2) * dt;
        p[i].vx += dvx;
        p[i].vy += dvy;
        f[i].x = 0;
        f[i].y = 0;
    }
}

void NonParallelCalc(int n)
{
    Init();
    for (int i = 0; i < n; i++)
    {
        CalcForces2();
        MoveParticlesAndFreeForces();
    }
}

// End of the non-parallel algorithm implementation

// Implementation of the parallel algorithm
// based on the conveyor method with stripes partitioning
Particle p2[N / 8], pp[N / 8];
Force f2[N / 8], ff[N / 8];
double m2[N / 8], mm[N / 8];
int count = 0;
MPI_Datatype MPI_PARTICLE, MPI_FORCE;

void ParallelCalc(int n)
{
    for (int i = 0; i < N / 8; i++)
    {
        int curr_i = (r + i * 8);
        p2[i].x = 20 * (curr_i / 20 - 20) + 10;
        p2[i].y = 20 * (curr_i % 20 - 10) + 10;
        p2[i].vx = p2[i].y / 15;
        p2[i].vy = -p2[i].x / 50;
        m2[i] = 100 + curr_i % 100;
        f2[i].x = 0;
        f2[i].y = 0;
    }
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < N / 8; j++)
        {
            pp[j] = p2[j];
            mm[j] = m2[j];
            ff[j] = f2[j];
        }
        for (int k = 0; k < N / 8 - 1; k++)
        {
            for (int j = k + 1; j < N / 8; j++)
            {
                double dx = p2[j].x - p2[k].x, dy = p2[j].y - p2[k].y,
                       r_2 = 1 / (dx * dx + dy * dy),
                       r_1 = sqrt(r_2),
                       fabs = gravity * m2[k] * m2[j] * r_2;
                if (fabs > fmax)
                {
                    fabs = fmax;
                }
                f2[k].x += dx = fabs * dx * r_1;
                f2[k].y += dy = fabs * dy * r_1;
                f2[j].x -= dx;
                f2[j].y -= dy;
            }
        }
        MPI_Status status;
        MPI_Sendrecv_replace(pp, N / 8, MPI_PARTICLE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
        MPI_Sendrecv_replace(ff, N / 8, MPI_FORCE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
        MPI_Sendrecv_replace(mm, N / 8, MPI_DOUBLE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
        for (int k = 1; k < 8; k++)
        {
            for (int h = 0; h < N / 8; h++)
            {
                for (int j = 0; j < N / 8; j++)
                {
                    if (((8 + r) % 8 + 8 * h) > ((8 + r - k) % 8 + 8 * j))
                    {
                        count++;
                        double dx = p2[h].x - pp[j].x, dy = p2[h].y - pp[j].y,
                               r_2 = 1 / (dx * dx + dy * dy),
                               r_1 = sqrt(r_2),
                               fabs = gravity * m2[h] * mm[j] * r_2;
                        if (fabs > fmax)
                        {
                            fabs = fmax;
                        }
                        ff[j].x += dx = fabs * dx * r_1;
                        ff[j].y += dy = fabs * dy * r_1;
                        f2[h].x -= dx;
                        f2[h].y -= dy;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            MPI_Status status;
            MPI_Sendrecv_replace(pp, N / 8, MPI_PARTICLE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
            MPI_Sendrecv_replace(ff, N / 8, MPI_FORCE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
            MPI_Sendrecv_replace(mm, N / 8, MPI_DOUBLE, (r + 1) % 8, 0, (r - 1 + 8) % 8, 0, MPI_COMM_WORLD, &status);
        }
        for (int j = 0; j < N / 8; j++)
        {
            double dvx = (f2[j].x + ff[j].x) * dt / m2[j],
                   dvy = (f2[j].y + ff[j].y) * dt / m2[j];
            p2[j].x += (p2[j].vx + dvx / 2) * dt;
            p2[j].y += (p2[j].vy + dvy / 2) * dt;
            p2[j].vx += dvx;
            p2[j].vy += dvy;
            f2[j].x = 0;
            f2[j].y = 0;
        }
    }
}

// End of parallel algorithm implementation

void Solve()
{
    Task("MPIGravit3");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    k = size;
    r = rank;
    if (r == 0)
    {
        pt >> Niter;
        // Testing the non-parallel algorithm:

        ShowLine("NON-PARALLEL ALGORITHM");
        NonParallelCalc(1);

        ShowLine("After one iteration");
        SetPrecision(12);
        Show("    Coordinates of the point 0:   ", p[0].x, 17);
        ShowLine(p[0].y, 17);
        Show("    Coordinates of the point 799: ", p[799].x, 17);
        ShowLine(p[799].y, 17);

        double t = MPI_Wtime();
        NonParallelCalc(Niter);
        t = MPI_Wtime() - t;

        ShowLine("After the required number of iterations");
        Show("    Coordinates of the point 0:   ", p[0].x, 17);
        ShowLine(p[0].y, 17);
        Show("    Coordinates of the point 799: ", p[799].x, 17);
        ShowLine(p[799].y, 17);

        SetPrecision(2);
        ShowLine("Non-parallel algorithm running time: ", t * 1000);
    }
    MPI_Bcast(&Niter, 1, MPI_INT, 0, MPI_COMM_WORLD);

    // Testing the parallel algorithm:
    MPI_Type_contiguous(4, MPI_DOUBLE, &MPI_PARTICLE);
    MPI_Type_commit(&MPI_PARTICLE);
    MPI_Type_contiguous(2, MPI_DOUBLE, &MPI_FORCE);
    MPI_Type_commit(&MPI_FORCE);
    ParallelCalc(1);
    if (r == 0)
    {
        pt << p2[0].x << p2[0].y;
    }
    else if (r == 7)
    {
        pt << p2[99].x << p2[99].y;
    }
    double t = MPI_Wtime();
    ParallelCalc(Niter);
    t = MPI_Wtime() - t;
    if (r == 0)
    {
        pt << p2[0].x << p2[0].y;
    }
    else if (r == 7)
    {
        pt << p2[99].x << p2[99].y;
    }
    Show("Parallel algorithm running time:  = ", t * 1000);
    Show("Count = ", count);
}