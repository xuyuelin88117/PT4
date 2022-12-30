#include "pt4.h"
#include "mpi.h"

void Solve()
{
    Task("MPI8Inter10");
    int flag;
    MPI_Initialized(&flag);
    if (flag == 0)
        return;
    int rank, size;
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);

    int c;
    pt >> c;
    if (c == 0)
        c = MPI_UNDEFINED;
    MPI_Comm comm;
    MPI_Comm_split(MPI_COMM_WORLD, c, rank, &comm);
    if (comm == MPI_COMM_NULL)
    {
        pt << -1;
        return;
    }
    int localRank;
    MPI_Comm_rank(comm, &localRank);
    pt << localRank;
    int remoteleader = rank == 0 ? size / 2 : 0;
    MPI_Comm inter;
    MPI_Intercomm_create(comm, 0, MPI_COMM_WORLD, remoteleader, 100, &inter);
    int R1, R2;
    pt >> R1 >> R2;
    int recv[3] = {0, 0, 0}, snd[3] = {0, 0, 0};
    int root;
    if (((localRank == R1) && (c == 1)) || ((localRank == R2) && (c == 2)))
    {
        for (int i = 0; i < 3; i++)
        {
            pt >> snd[i];
            recv[i] = snd[i];
        }
    }
    // 1=>2
    if (c == 1)
        root = localRank == R1 ? MPI_ROOT : MPI_PROC_NULL;
    else
        root = R1;
    MPI_Bcast(recv, 3, MPI_INT, root, inter);

    // 2=>1
    if (c == 2)
        root = localRank == R2 ? MPI_ROOT : MPI_PROC_NULL;
    else
        root = R2;
    MPI_Bcast(snd, 3, MPI_INT, root, inter);
    if (c == 1)
        for (int i = 0; i < 3; i++)
            pt << snd[i];
    else
        for (int i = 0; i < 3; i++)
            pt << recv[i];

    //利用两个数组来避免数据被覆盖
}
/*
进程的数量K是一个偶数。每个过程中都有一个整数C。整数C的范围是0到2，第一个值C=1是在进程0中给出的，
第一个值C=2是在进程K/2中给出的。使用MPI_Comm_split函数，创建两个通信器：
第一个通信器包含C=1的进程（顺序相同），第二个通信器包含C=2的进程（顺序相同）。
输出包含在这些通信器中的进程的等级R（如果进程没有被包含在创建的通信器中，则输出整数-1）。
然后使用MPI_Intercomm_create函数将这些通信器组合成一个通信器。
含有C=1的进程的组被认为是所创建的通信器的第一组，C=2的进程组被认为是其第二组。
在相互通信器的每个进程中输入整数R1和R2。
数字R1的值在所有进程中都是重合的，表示第一组中所选进程的等级；
数字R2的值在所有进程中也是重合的，表示第二组中所选进程的等级。
第一组的选定进程中给出了三个整数X的序列，第二组的选定进程中给出了三个整数Y的序列。
在通信器的每个进程中使用两次MPI_Bcast集体函数的调用，将数字X发送到第二组的所有进程，
将数字Y发送到第一组的所有进程，并输出收到的数字。
*/